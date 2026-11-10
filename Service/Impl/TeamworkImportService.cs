using Mep01Web.DTO;
using Mep01Web.DTO.Request;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Mep01Web.Service.Interface;
using MepWeb.Costants;
using MepWeb.DTO.Request;
using MepWeb.DTO.Response;
using MepWeb.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MepWeb.Service.Impl
{
    public class TeamworkImportService : ITeamworkImportService
    {
        private static readonly TimeZoneInfo FusoItalia = TrovaFusoItalia();
        private static readonly string[] CausaliIsl = { "SVIL", "DELI", "ANFU" };
        private const int ToleranzaDoppioneSecondi = 15 * 60;
        private const int MaxNote = 2000;

        private readonly SataconsultingContext _db;
        private readonly ITeamworkAuthService _auth;
        private readonly ITeamworkApiService _api;
        private readonly ICrrgTwService _crrgTw;
        private readonly ICrrgService _crrgService;
        private readonly IMvxpa01Service _mvxpa01Service;
        private readonly UserScope _userScope;
        private readonly ILogger<TeamworkImportService> _logger;

        public TeamworkImportService(SataconsultingContext db, ITeamworkAuthService auth, ITeamworkApiService api,
            ICrrgTwService crrgTw, ICrrgService crrgService, IMvxpa01Service mvxpa01Service, UserScope userScope,
            ILogger<TeamworkImportService> logger)
        {
            _db = db;
            _auth = auth;
            _api = api;
            _crrgTw = crrgTw;
            _crrgService = crrgService;
            _mvxpa01Service = mvxpa01Service;
            _userScope = userScope;
            _logger = logger;
        }

        // ---------------------------------------------------------------- Anteprima

        public async Task<ResponseBase<TwAnteprimaResponse>> GetAnteprimaAsync(DateTime dal, DateTime al)
        {
            var conn = await GetConnessioneAsync();
            if (conn == null)
            {
                return ResponseBase<TwAnteprimaResponse>.Failed("nonCollegato", "Account Teamwork non collegato: collegalo dal menu in alto a destra.");
            }
            var sigla = Sigla();
            var risultato = new TwAnteprimaResponse { Dal = dal.Date, Al = al.Date };

            // 1. Registrazioni del periodo su Teamwork
            var regEsito = await _api.GetRegistrazioniAsync(conn, dal, al);
            if (!regEsito.Succeeded || regEsito.Body == null)
            {
                return ResponseBase<TwAnteprimaResponse>.Failed(regEsito.Errors!);
            }
            var registrazioni = regEsito.Body.Where(r => !r.Cancellata).GroupBy(r => r.Id).ToDictionary(g => g.Key, g => g.First());

            // 2. Collegamenti: quelli delle registrazioni lette + quelli importati da me con data nel periodo
            var dalUtc = DaOraLocale(dal.Date);
            var alUtc = DaOraLocale(al.Date.AddDays(1)).AddSeconds(-1);
            var collegamenti = (await _crrgTw.GetByTimelogIdsAsync(registrazioni.Keys))
                .Concat(string.IsNullOrEmpty(sigla) ? Enumerable.Empty<FlussoCrrgTw>() : await _crrgTw.GetByPeriodoAsync(sigla, dalUtc, alUtc))
                .GroupBy(c => c.TwTimelogId).Select(g => g.First())
                .ToDictionary(c => c.TwTimelogId);

            // 3. Collegamenti non presenti nella lettura per periodo: spostati su altre date o cancellati -> controllo per ID
            var mancanti = collegamenti.Keys.Where(id => !registrazioni.ContainsKey(id)).ToList();
            var perId = new Dictionary<long, TwRegistrazione>();
            if (mancanti.Count > 0)
            {
                var idEsito = await _api.GetRegistrazioniPerIdAsync(conn, mancanti);
                if (!idEsito.Succeeded || idEsito.Body == null)
                {
                    return ResponseBase<TwAnteprimaResponse>.Failed(idEsito.Errors!);
                }
                perId = idEsito.Body.GroupBy(r => r.Id).ToDictionary(g => g.Key, g => g.First());
            }

            // 4. Task con il campo Commessa/ISL
            var taskIds = registrazioni.Values.Concat(perId.Values).Where(r => r.TaskId != null).Select(r => r.TaskId!.Value)
                .Concat(collegamenti.Values.Select(c => c.TwTaskId)).Distinct().ToList();
            var taskEsito = await LeggiTaskAsync(conn, taskIds);
            if (!taskEsito.Succeeded || taskEsito.Body == null)
            {
                return ResponseBase<TwAnteprimaResponse>.Failed(taskEsito.Errors!);
            }
            var tasks = taskEsito.Body.Tasks;
            if (!taskEsito.Body.CampiPersonalizzatiPresenti)
            {
                risultato.Avvisi.Add("Teamwork non ha restituito i campi personalizzati dei task: il campo Commessa/ISL risulta vuoto per tutte le righe.");
            }

            // 5. Commesse MEP per ogni valore distinto del campo Commessa/ISL
            var commesse = await RisolviCommesseAsync(tasks.Values.Select(t => t.Commessa).Concat(collegamenti.Values.Select(c => c.TwCommessa)));

            // 6. Consuntivi ancora esistenti, con le note (mostrate nelle righe già importate)
            var srlCollegati = collegamenti.Values.Select(c => c.CrrgCSrl).Distinct().ToList();
            string cDitta = CommonCostants.CDitta;
            var consuntiviEsistenti = srlCollegati.Count == 0
                ? new Dictionary<decimal, string?>()
                : (await _db.FlussoCrrgs.AsNoTracking()
                    .Where(c => srlCollegati.Contains(c.CrrgCSrl) && c.CrrgCDitta == cDitta)
                    .Select(c => new { c.CrrgCSrl, c.CrrgNote }).ToListAsync())
                    .ToDictionary(c => c.CrrgCSrl, c => (string?)c.CrrgNote);

            // 7. Righe
            foreach (var reg in registrazioni.Values.OrderBy(r => r.TimeLoggedUtc))
            {
                var task = reg.TaskId != null && tasks.TryGetValue(reg.TaskId.Value, out var t) ? t : null;
                if (!collegamenti.TryGetValue(reg.Id, out var coll))
                {
                    risultato.Nuove.Add(CreaRiga(reg, task, commesse, TwStatoRegistrazione.Nuova, null, new List<string>()));
                    continue;
                }
                AggiungiCollegata(risultato, reg, task, coll, commesse, consuntiviEsistenti);
            }
            foreach (var coll in collegamenti.Values.Where(c => !registrazioni.ContainsKey(c.TwTimelogId)).OrderBy(c => c.TwDate))
            {
                if (perId.TryGetValue(coll.TwTimelogId, out var reg) && !reg.Cancellata)
                {
                    var task = reg.TaskId != null && tasks.TryGetValue(reg.TaskId.Value, out var t) ? t : null;
                    AggiungiCollegata(risultato, reg, task, coll, commesse, consuntiviEsistenti);
                }
                else
                {
                    // Cancellata su Teamwork (deleted = true) oppure non più restituita
                    var task = tasks.TryGetValue(coll.TwTaskId, out var t) ? t : null;
                    var regDaCollegamento = new TwRegistrazione
                    {
                        Id = coll.TwTimelogId, TaskId = coll.TwTaskId, Minuti = coll.TwMinutes,
                        TimeLoggedUtc = DateTime.SpecifyKind(coll.TwDate, DateTimeKind.Utc), Descrizione = coll.TwDescrizioneReg
                    };
                    var diff = new List<string> { reg == null ? "Registrazione non più presente su Teamwork" : "Registrazione cancellata su Teamwork" };
                    risultato.DaAllineare.Add(CreaRiga(regDaCollegamento, task, commesse, TwStatoRegistrazione.CancellataInTeamwork, coll.CrrgCSrl, diff,
                        commessaTeamwork: coll.TwCommessa));
                }
            }

            // 8. Valori proposti e tendine per le righe da importare
            await PreparaImportAsync(risultato, sigla);

            return ResponseBase<TwAnteprimaResponse>.Success(risultato);
        }

        /// <summary>Tendine (operazioni per commessa, macroattività, causali) e valori proposti per le righe nuove importabili.</summary>
        private async Task PreparaImportAsync(TwAnteprimaResponse risultato, string? sigla)
        {
            var importabili = risultato.Nuove.Where(r => r.NonImportabile == null && r.CommessaMep?.TstDoc != null).ToList();
            if (importabili.Count == 0)
            {
                return;
            }

            risultato.CausaliIsl = CausaliIsl.Select(c => new TwOpzione { Valore = c, Testo = c }).ToList();
            var macro = await _mvxpa01Service.GetMvxpa01Async();
            risultato.Macroattivita = (macro?.Body ?? new List<Mep01Web.Models.Views.Mvxpa01>())
                .Select(m => new TwOpzione { Valore = m.Cmaatt, Testo = m.Descrizione ?? m.Cmaatt }).ToList();

            foreach (var comm in importabili.Select(r => r.CommessaMep!).GroupBy(c => c.Codice).Select(g => g.First()))
            {
                risultato.OperazioniPerCommessa[comm.Codice] = await GetOperazioniAsync(comm);
            }

            // Ultimo consuntivo importato per ogni task: si ripropongono operazione, causale e macroattività
            var ultimoPerTask = new Dictionary<long, FlussoCrrg?>();
            foreach (var taskId in importabili.Where(r => r.TaskId != null).Select(r => r.TaskId!.Value).Distinct())
            {
                ultimoPerTask[taskId] = await _crrgTw.GetUltimoConsuntivoPerTaskAsync(taskId);
            }

            // Possibili doppioni: consuntivi inseriti a mano (non collegati) con stessa data, commessa e durata simile
            var manuali = await GetConsuntiviManualiAsync(sigla, risultato.Dal, risultato.Al);

            foreach (var r in importabili)
            {
                var comm = r.CommessaMep!;
                var operazioni = risultato.OperazioniPerCommessa[comm.Codice];
                var ultimo = r.TaskId != null && ultimoPerTask.TryGetValue(r.TaskId.Value, out var u) ? u : null;

                var opUltimo = ultimo == null ? null : $"{ultimo.CrrgNOper:0}-{ultimo.CrrgTOper}";
                r.PropostaNTOper = opUltimo != null && operazioni.Any(o => o.Valore == opUltimo) ? opUltimo
                    : operazioni.Count == 1 ? operazioni[0].Valore : null;

                r.PropostaCausale = comm.CausaleFissaCori ? "CORI"
                    : ultimo?.CrrgCCaus is string c && CausaliIsl.Contains(c) ? c : null;

                r.PropostaMacroattivita = comm.VerbaleObbligatorio ? "3"
                    : ultimo?.CrrgCmaatt is string m && risultato.Macroattivita.Any(x => x.Valore == m) && (comm.PrfDoc != "E" || m is "1" or "2" or "3") ? m
                    : comm.PrfDoc == "E" ? null
                    : risultato.Macroattivita.Any(x => x.Valore == "0") ? "0" : null;

                r.PropostaNote = ComponiNote(r.TaskNome, r.Descrizione);

                var doppione = manuali.FirstOrDefault(x => x.Data == r.DataOra.Date && x.Codice == comm.Codice
                    && Math.Abs(x.Secondi - r.Minuti * 60) <= ToleranzaDoppioneSecondi);
                if (doppione != null)
                {
                    r.AvvisoDoppione = $"Possibile doppione: consuntivo {doppione.CrrgCSrl:0} del {doppione.Data:dd/MM} ({doppione.Secondi / 3600}:{doppione.Secondi % 3600 / 60:00}) sulla stessa commessa";
                }
            }
        }

        private void AggiungiCollegata(TwAnteprimaResponse risultato, TwRegistrazione reg, TwTask? task, FlussoCrrgTw coll,
            Dictionary<string, TwCommessaMep> commesse, Dictionary<decimal, string?> consuntiviEsistenti)
        {
            if (!consuntiviEsistenti.TryGetValue(coll.CrrgCSrl, out var noteConsuntivo))
            {
                risultato.DaAllineare.Add(CreaRiga(reg, task, commesse, TwStatoRegistrazione.ConsuntivoEliminatoInMep, coll.CrrgCSrl,
                    new List<string> { $"Il consuntivo {coll.CrrgCSrl:0} non esiste più in MEP" }));
                return;
            }
            if (task == null || task.Cancellato)
            {
                // Su Teamwork la registrazione resta "non cancellata" ma il suo task è nel cestino (o non c'è più)
                risultato.DaAllineare.Add(CreaRiga(reg, task, commesse, TwStatoRegistrazione.CancellataInTeamwork, coll.CrrgCSrl,
                    new List<string> { task == null
                        ? "Task non più presente su Teamwork"
                        : "Task cancellato su Teamwork: la registrazione è nel cestino insieme al task" },
                    commessaTeamwork: coll.TwCommessa));
                return;
            }

            var confronto = Confronta(coll, reg, task);
            var stato = confronto.Tutte.Count == 0 ? TwStatoRegistrazione.Importata : TwStatoRegistrazione.ModificataInTeamwork;
            var riga = CreaRiga(reg, task, commesse, stato, coll.CrrgCSrl, confronto.Tutte, soloRiallineamento: !confronto.CambiaConsuntivo);
            riga.NoteConsuntivo = noteConsuntivo;
            (stato == TwStatoRegistrazione.Importata ? risultato.Importate : risultato.DaAllineare).Add(riga);
        }

        /// <summary>Differenze tra la fotografia salvata all'import e la registrazione attuale su Teamwork.</summary>
        private sealed class Confronto
        {
            public bool OreCambiate { get; init; }
            public bool DataCambiata { get; init; }
            public bool DescrizioneCambiata { get; init; }
            public bool CommessaCambiata { get; init; }
            /// <summary>Differenze che modificano il consuntivo.</summary>
            public List<string> Sostanziali { get; } = new();
            /// <summary>Differenze che richiedono solo di riallineare la fotografia (orario, fatturabile...).</summary>
            public List<string> Secondarie { get; } = new();
            public bool CambiaConsuntivo => OreCambiate || DataCambiata || DescrizioneCambiata || CommessaCambiata;
            public List<string> Tutte => Sostanziali.Concat(Secondarie).ToList();
        }

        private static Confronto Confronta(FlussoCrrgTw coll, TwRegistrazione reg, TwTask? task)
        {
            var primaLocale = InOraLocale(coll.TwDate);
            var oraLocale = InOraLocale(reg.TimeLoggedUtc);
            var c = new Confronto
            {
                OreCambiate = coll.TwMinutes != reg.Minuti,
                DataCambiata = primaLocale.Date != oraLocale.Date,
                DescrizioneCambiata = (coll.TwDescrizioneReg ?? "") != Tronca(reg.Descrizione ?? "", MaxNote),
                CommessaCambiata = !string.Equals(coll.TwCommessa ?? "", task?.Commessa ?? "", StringComparison.OrdinalIgnoreCase)
            };
            if (c.OreCambiate)
                c.Sostanziali.Add($"Ore: {Durata(coll.TwMinutes)} → {Durata(reg.Minuti)}");
            if (c.DataCambiata)
                c.Sostanziali.Add($"Data: {primaLocale:dd/MM/yyyy} → {oraLocale:dd/MM/yyyy}");
            if (c.DescrizioneCambiata)
                c.Sostanziali.Add("Descrizione modificata: le note del consuntivo verranno riscritte");
            if (c.CommessaCambiata)
                c.Sostanziali.Add($"Commessa/ISL del task: {coll.TwCommessa ?? "(vuoto)"} → {task?.Commessa ?? "(vuoto)"}");

            if (!c.DataCambiata && TroncaSecondi(coll.TwDate) != TroncaSecondi(reg.TimeLoggedUtc))
                c.Secondarie.Add($"Orario di inizio: {primaLocale:HH:mm} → {oraLocale:HH:mm} (nel consuntivo si salva solo la data, quindi resta uguale)");
            var aggiornataSuTeamwork = reg.UpdatedAtUtc != null && TroncaSecondi(reg.UpdatedAtUtc.Value) != TroncaSecondi(coll.TwUpdatedAt);
            if (!c.CambiaConsuntivo && c.Secondarie.Count == 0 && aggiornataSuTeamwork)
                c.Secondarie.Add("Modificati solo dati secondari (es. fatturabile): basta riallineare");
            return c;
        }

        private static TwRigaAnteprima CreaRiga(TwRegistrazione reg, TwTask? task, Dictionary<string, TwCommessaMep> commesse,
            TwStatoRegistrazione stato, decimal? crrgCSrl, List<string> differenze, string? commessaTeamwork = null, bool soloRiallineamento = false)
        {
            var valore = commessaTeamwork ?? task?.Commessa;
            TwCommessaMep? commessa = !string.IsNullOrWhiteSpace(valore) && commesse.TryGetValue(valore, out var c) ? c : null;

            return new TwRigaAnteprima
            {
                TimelogId = reg.Id,
                DataOra = InOraLocale(reg.TimeLoggedUtc),
                Minuti = reg.Minuti,
                Descrizione = reg.Descrizione,
                TaskId = reg.TaskId,
                TaskNome = task?.Nome,
                CommessaTeamwork = valore,
                CommessaMep = commessa,
                Stato = stato,
                CrrgCSrl = crrgCSrl,
                Differenze = differenze,
                SoloRiallineamento = soloRiallineamento,
                NonImportabile = MotivoNonImportabile(reg, task, valore, commessa)
            };
        }

        private static string? MotivoNonImportabile(TwRegistrazione reg, TwTask? task, string? valoreCommessa, TwCommessaMep? commessa)
        {
            if (reg.TaskId == null) return "Registrazione senza task: manca il campo Commessa/ISL";
            if (task == null) return "Task non trovato su Teamwork (cancellato o non visibile)";
            if (task.Cancellato) return "Task cancellato su Teamwork";
            if (string.IsNullOrWhiteSpace(valoreCommessa)) return "Campo Commessa/ISL del task vuoto";
            if (commessa?.Errore != null) return commessa.Errore;
            if (reg.Minuti <= 0) return "Durata nulla";
            if (reg.Minuti >= 24 * 60) return "Durata di 24 ore o più";
            return null;
        }

        // ---------------------------------------------------------------- Import

        public async Task<ResponseBase<TwImportEsito>> ImportaAsync(TwImportRequest request)
        {
            var selezionate = request.Righe.Where(r => r.Seleziona).GroupBy(r => r.TimelogId).Select(g => g.First()).ToList();
            if (selezionate.Count == 0)
            {
                return ResponseBase<TwImportEsito>.Failed("nessuna", "Nessuna registrazione selezionata.");
            }

            var conn = await GetConnessioneAsync();
            if (conn == null)
            {
                return ResponseBase<TwImportEsito>.Failed("nonCollegato", "Account Teamwork non collegato: collegalo dal menu in alto a destra.");
            }
            var sigla = Sigla();
            if (string.IsNullOrEmpty(sigla))
            {
                return ResponseBase<TwImportEsito>.Failed("sigla", "Sigla dell'utente MEPWEB non disponibile.");
            }

            // Dati sempre riletti da Teamwork: dal form arrivano solo le scelte dell'utente
            var regEsito = await _api.GetRegistrazioniPerIdAsync(conn, selezionate.Select(r => r.TimelogId));
            if (!regEsito.Succeeded || regEsito.Body == null)
            {
                return ResponseBase<TwImportEsito>.Failed(regEsito.Errors!);
            }
            var registrazioni = regEsito.Body.Where(r => !r.Cancellata && r.UserId == conn.TwUserId)
                .GroupBy(r => r.Id).ToDictionary(g => g.Key, g => g.First());

            var taskEsito = await LeggiTaskAsync(conn, registrazioni.Values.Where(r => r.TaskId != null).Select(r => r.TaskId!.Value).Distinct().ToList());
            if (!taskEsito.Succeeded || taskEsito.Body == null)
            {
                return ResponseBase<TwImportEsito>.Failed(taskEsito.Errors!);
            }
            var tasks = taskEsito.Body.Tasks;
            var commesse = await RisolviCommesseAsync(tasks.Values.Select(t => t.Commessa));
            var giaCollegate = (await _crrgTw.GetByTimelogIdsAsync(registrazioni.Keys)).Select(c => c.TwTimelogId).ToHashSet();

            var esito = new TwImportEsito();
            foreach (var riga in selezionate)
            {
                if (!registrazioni.TryGetValue(riga.TimelogId, out var reg))
                {
                    esito.Errori.Add($"Registrazione {riga.TimelogId}: non trovata su Teamwork o non tua.");
                    continue;
                }
                var etichetta = $"{InOraLocale(reg.TimeLoggedUtc):dd/MM} {Durata(reg.Minuti)}";
                if (giaCollegate.Contains(reg.Id))
                {
                    esito.Errori.Add($"{etichetta}: già importata.");
                    continue;
                }

                var task = reg.TaskId != null && tasks.TryGetValue(reg.TaskId.Value, out var t) ? t : null;
                var commessa = !string.IsNullOrWhiteSpace(task?.Commessa) && commesse.TryGetValue(task!.Commessa!, out var c) ? c : null;
                var motivo = MotivoNonImportabile(reg, task, task?.Commessa, commessa);
                if (motivo != null || commessa?.TstDoc == null)
                {
                    esito.Errori.Add($"{etichetta}: {motivo ?? "commessa non determinata"}.");
                    continue;
                }

                var crrgRequest = new CrrgCreateRequest
                {
                    CrrgCRis = sigla.ToUpperInvariant(),
                    CrrgCdl = null!,                                     // il validatore lo ricava dalla risorsa (flusso_macc)
                    CrrgDtt = InOraLocale(reg.TimeLoggedUtc).Date,
                    CrrgTmRunIncrHMS = DateTime.Today.AddMinutes(reg.Minuti),
                    CrrgRifCliente = commessa.Isl,
                    CommCode = commessa.Isl == null ? commessa.Codice : null!,
                    CrrgTstDoc = commessa.TstDoc!, CrrgPrfDoc = commessa.PrfDoc!, CrrgADoc = commessa.ADoc, CrrgNDoc = commessa.NDoc,
                    NTOper = riga.NTOper ?? "",
                    CrrgCCaus = commessa.CausaleFissaCori ? "CORI" : (riga.Causale ?? ""),
                    CrrgCmaatt = commessa.VerbaleObbligatorio ? "3" : (riga.Macroattivita ?? ""),
                    CrrgNote = Tronca(string.IsNullOrWhiteSpace(riga.Note) ? ComponiNote(task!.Nome, reg.Descrizione) : riga.Note.Trim(), MaxNote),
                    CrrgApp = null!,
                    CrrgMod = null!
                };

                var collegamento = new FlussoCrrgTw
                {
                    TwTimelogId = reg.Id,
                    TwTaskId = reg.TaskId!.Value,
                    TwMinutes = reg.Minuti,
                    TwDate = TroncaSecondi(reg.TimeLoggedUtc),
                    TwCommessa = task!.Commessa,
                    TwDescrizioneReg = string.IsNullOrEmpty(reg.Descrizione) ? null : Tronca(reg.Descrizione, MaxNote),
                    TwUpdatedAt = TroncaSecondi(reg.UpdatedAtUtc ?? DateTime.UtcNow),
                    Sigla = sigla
                };

                var errore = await ImportaUnaAsync(crrgRequest, collegamento);
                if (errore == null)
                {
                    esito.Importate.Add(new TwImportato { Nome = task.Nome ?? $"Task {task.Id}", CrrgCSrl = collegamento.CrrgCSrl });
                }
                else
                {
                    esito.Errori.Add($"{etichetta} {task.Nome}: {errore}");
                }
            }

            return ResponseBase<TwImportEsito>.Success(esito);
        }

        /// <summary>Consuntivo + collegamento in un'unica transazione. Ritorna null se riuscito, altrimenti il messaggio d'errore.</summary>
        private async Task<string?> ImportaUnaAsync(CrrgCreateRequest crrgRequest, FlussoCrrgTw collegamento)
        {
            await using var transazione = await _db.Database.BeginTransactionAsync();
            try
            {
                var crrg = await _crrgService.AddCrrgAsync(crrgRequest);
                if (crrg == null || !crrg.Succeeded || crrg.Body?.FlussoCrrg == null)
                {
                    await AnnullaAsync(transazione);
                    return crrg?.Errors?.FirstOrDefault()?.Message ?? "Creazione del consuntivo non riuscita";
                }

                collegamento.CrrgCSrl = crrg.Body.FlussoCrrg.CrrgCSrl;
                var link = await _crrgTw.InsertAsync(collegamento);
                if (!link.Succeeded)
                {
                    await AnnullaAsync(transazione);
                    return link.Errors?.FirstOrDefault()?.Message ?? "Salvataggio del collegamento non riuscito";
                }

                await transazione.CommitAsync();
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Import della registrazione Teamwork {TimelogId} non riuscito", collegamento.TwTimelogId);
                await AnnullaAsync(transazione);
                return $"Errore imprevisto: {ex.GetBaseException().Message}";
            }
        }

        private async Task AnnullaAsync(Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transazione)
        {
            await transazione.RollbackAsync();
            // Le entità rimaste in sospeso (consuntivo, giorni ISL) non devono finire nel salvataggio della riga successiva
            _db.ChangeTracker.Clear();
        }

        // ---------------------------------------------------------------- Allineamento

        public async Task<ResponseBase<string>> AllineaAsync(long timelogId, TwAzioneAllineamento azione)
        {
            var conn = await GetConnessioneAsync();
            if (conn == null)
            {
                return ResponseBase<string>.Failed("nonCollegato", "Account Teamwork non collegato: collegalo dal menu in alto a destra.");
            }
            var sigla = Sigla();
            var coll = await _crrgTw.GetByTimelogIdAsync(timelogId);
            if (coll == null)
            {
                return ResponseBase<string>.Failed("collegamento", "Collegamento non trovato: ricarica la pagina.");
            }
            if (!string.Equals(coll.Sigla?.Trim(), sigla, StringComparison.OrdinalIgnoreCase))
            {
                return ResponseBase<string>.Failed("utente", "La registrazione è stata importata da un altro utente.");
            }

            // Stato attuale su Teamwork e in MEP
            var regEsito = await _api.GetRegistrazioniPerIdAsync(conn, new[] { timelogId });
            if (!regEsito.Succeeded || regEsito.Body == null)
            {
                return ResponseBase<string>.Failed(regEsito.Errors!);
            }
            var reg = regEsito.Body.FirstOrDefault(r => r.Id == timelogId);
            if (reg != null && !reg.Cancellata && reg.UserId != null && reg.UserId != conn.TwUserId)
            {
                return ResponseBase<string>.Failed("utente", "La registrazione Teamwork non è tua.");
            }
            var cancellata = reg == null || reg.Cancellata;
            if (!cancellata && reg!.TaskId != null)
            {
                // Registrazione su un task cancellato: per Teamwork non è cancellata, per MEPWEB sì
                var taskEsito = await LeggiTaskAsync(conn, new List<long> { reg.TaskId.Value });
                if (!taskEsito.Succeeded || taskEsito.Body == null)
                {
                    return ResponseBase<string>.Failed(taskEsito.Errors!);
                }
                cancellata = !taskEsito.Body.Tasks.TryGetValue(reg.TaskId.Value, out var taskAttuale) || taskAttuale.Cancellato;
            }
            string cDitta = CommonCostants.CDitta;
            var consuntivo = await _db.FlussoCrrgs.AsNoTracking()
                .FirstOrDefaultAsync(c => c.CrrgCSrl == coll.CrrgCSrl && c.CrrgCDitta == cDitta);

            switch (azione)
            {
                case TwAzioneAllineamento.Scollega:
                    if (!cancellata && consuntivo != null)
                    {
                        return ResponseBase<string>.Failed("stato", "La registrazione esiste ancora su Teamwork e il consuntivo c'è: usa «Aggiorna consuntivo».");
                    }
                    await _crrgTw.DeleteAsync(timelogId);
                    return ResponseBase<string>.Success(consuntivo == null
                        ? "Collegamento eliminato: la registrazione torna tra quelle da importare."
                        : $"Collegamento eliminato: il consuntivo {coll.CrrgCSrl:0} resta ed è di nuovo modificabile in MEPWEB.");

                case TwAzioneAllineamento.Elimina:
                    if (!cancellata)
                    {
                        return ResponseBase<string>.Failed("stato", "La registrazione esiste ancora su Teamwork: il consuntivo non si elimina.");
                    }
                    if (consuntivo == null)
                    {
                        await _crrgTw.DeleteAsync(timelogId);
                        return ResponseBase<string>.Success("Il consuntivo non c'era più: eliminato il collegamento.");
                    }
                    return await EliminaConsuntivoAsync(coll, consuntivo);

                case TwAzioneAllineamento.Aggiorna:
                    if (cancellata)
                    {
                        return ResponseBase<string>.Failed("stato", "La registrazione è stata cancellata su Teamwork: scegli se scollegare o eliminare il consuntivo.");
                    }
                    if (consuntivo == null)
                    {
                        return ResponseBase<string>.Failed("stato", "Il consuntivo non esiste più in MEP: usa «Scollega» per poterla reimportare.");
                    }
                    return await AggiornaConsuntivoAsync(conn, coll, reg!, consuntivo);

                default:
                    return ResponseBase<string>.Failed("azione", "Azione non riconosciuta.");
            }
        }

        private async Task<ResponseBase<string>> EliminaConsuntivoAsync(FlussoCrrgTw coll, FlussoCrrg consuntivo)
        {
            await using var transazione = await _db.Database.BeginTransactionAsync();
            try
            {
                // Passa da CrrgService per stornare anche i giorni ISL (DELI/SVIL)
                var richiesta = new CrrgCreateRequest(coll.CrrgCSrl)
                {
                    CrrgRifCliente = consuntivo.CrrgRifCliente,
                    CrrgCCaus = consuntivo.CrrgCCaus!,
                    DaAllineamentoTeamwork = true
                };
                var esito = await _crrgService.DeleteCrrgAsync(richiesta);
                if (esito == null || !esito.Succeeded)
                {
                    await AnnullaAsync(transazione);
                    return ResponseBase<string>.Failed("crrg", esito?.Errors?.FirstOrDefault()?.Message ?? "Eliminazione del consuntivo non riuscita.");
                }
                var link = await _crrgTw.DeleteAsync(coll.TwTimelogId);
                if (!link.Succeeded)
                {
                    await AnnullaAsync(transazione);
                    return ResponseBase<string>.Failed("link", link.Errors?.FirstOrDefault()?.Message ?? "Eliminazione del collegamento non riuscita.");
                }
                await transazione.CommitAsync();
                return ResponseBase<string>.Success($"Consuntivo {coll.CrrgCSrl:0} e collegamento eliminati.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eliminazione del consuntivo {CrrgCSrl} collegato a Teamwork non riuscita", coll.CrrgCSrl);
                await AnnullaAsync(transazione);
                return ResponseBase<string>.Failed("errore", $"Errore imprevisto: {ex.GetBaseException().Message}");
            }
        }

        private async Task<ResponseBase<string>> AggiornaConsuntivoAsync(TeamworkConnessione conn, FlussoCrrgTw coll, TwRegistrazione reg, FlussoCrrg consuntivo)
        {
            var taskEsito = await LeggiTaskAsync(conn, reg.TaskId == null ? new List<long>() : new List<long> { reg.TaskId.Value });
            if (!taskEsito.Succeeded || taskEsito.Body == null)
            {
                return ResponseBase<string>.Failed(taskEsito.Errors!);
            }
            var task = reg.TaskId != null && taskEsito.Body.Tasks.TryGetValue(reg.TaskId.Value, out var t) ? t : null;
            var commesse = await RisolviCommesseAsync(new[] { task?.Commessa });
            var commessa = !string.IsNullOrWhiteSpace(task?.Commessa) && commesse.TryGetValue(task!.Commessa!, out var c) ? c : null;
            var motivo = MotivoNonImportabile(reg, task, task?.Commessa, commessa);
            if (motivo != null || commessa?.TstDoc == null)
            {
                return ResponseBase<string>.Failed("commessa", $"Non allineabile: {motivo ?? "commessa non determinata"}.");
            }

            var confronto = Confronta(coll, reg, task);
            var fotografia = new FlussoCrrgTw
            {
                TwTimelogId = coll.TwTimelogId,
                TwTaskId = reg.TaskId!.Value,
                TwMinutes = reg.Minuti,
                TwDate = TroncaSecondi(reg.TimeLoggedUtc),
                TwCommessa = task!.Commessa,
                TwDescrizioneReg = string.IsNullOrEmpty(reg.Descrizione) ? null : Tronca(reg.Descrizione, MaxNote),
                TwUpdatedAt = TroncaSecondi(reg.UpdatedAtUtc ?? DateTime.UtcNow)
            };

            if (!confronto.CambiaConsuntivo)
            {
                var solo = await _crrgTw.AggiornaFotografiaAsync(fotografia);
                return solo.Succeeded
                    ? ResponseBase<string>.Success("Registrazione riallineata: il consuntivo non cambia.")
                    : ResponseBase<string>.Failed("link", solo.Errors?.FirstOrDefault()?.Message ?? "Riallineamento non riuscito.");
            }

            // Il tempo "produttivo" segue le ore solo se finora coincideva con il tempo registrato
            var secondi = reg.Minuti * 60;
            var secondiProd = consuntivo.CrrgTmRunIncrProd == 0 || consuntivo.CrrgTmRunIncrProd == consuntivo.CrrgTmRunIncr
                ? secondi : (int)consuntivo.CrrgTmRunIncrProd;

            var richiesta = new CrrgCreateRequest(consuntivo.CrrgCSrl)
            {
                IsUpdate = true,
                DaAllineamentoTeamwork = true,
                CrrgCRis = consuntivo.CrrgCRis!,
                CrrgCdl = consuntivo.CrrgCdl!,
                CrrgDtt = InOraLocale(reg.TimeLoggedUtc).Date,
                CrrgTmRunIncrHMS = DateTime.Today.AddSeconds(secondi),
                CrrgTmRunIncrHMSProd = DateTime.Today.AddSeconds(secondiProd),
                CrrgRifCliente = commessa.Isl,
                CommCode = commessa.Isl == null ? commessa.Codice : null!,
                CrrgTstDoc = commessa.TstDoc!, CrrgPrfDoc = commessa.PrfDoc!, CrrgADoc = commessa.ADoc, CrrgNDoc = commessa.NDoc,
                CrrgPosDoc = consuntivo.CrrgPosDoc, CrrgPrgDoc = consuntivo.CrrgPrgDoc,
                NTOper = $"{consuntivo.CrrgNOper:0}-{consuntivo.CrrgTOper}",
                CrrgNOper = consuntivo.CrrgNOper, CrrgTOper = consuntivo.CrrgTOper!,
                CrrgCCaus = commessa.CausaleFissaCori ? "CORI" : consuntivo.CrrgCCaus!,
                CrrgCmaatt = commessa.VerbaleObbligatorio ? "3" : consuntivo.CrrgCmaatt!,
                CrrgNote = confronto.DescrizioneCambiata ? ComponiNote(task.Nome, reg.Descrizione) : consuntivo.CrrgNote,
                CrrgApp = consuntivo.CrrgApp!,
                CrrgMod = consuntivo.CrrgMod!
            };

            await using var transazione = await _db.Database.BeginTransactionAsync();
            try
            {
                var esito = await _crrgService.AddCrrgAsync(richiesta);   // con IsUpdate valida e poi aggiorna
                if (esito == null || !esito.Succeeded)
                {
                    await AnnullaAsync(transazione);
                    var errore = esito?.Errors?.FirstOrDefault()?.Message ?? "Aggiornamento del consuntivo non riuscito";
                    if (confronto.CommessaCambiata)
                    {
                        errore += ". La commessa è cambiata: se operazione o causale non valgono sulla nuova commessa, elimina il collegamento e il consuntivo e reimporta la registrazione.";
                    }
                    return ResponseBase<string>.Failed("crrg", errore);
                }
                var link = await _crrgTw.AggiornaFotografiaAsync(fotografia);
                if (!link.Succeeded)
                {
                    await AnnullaAsync(transazione);
                    return ResponseBase<string>.Failed("link", link.Errors?.FirstOrDefault()?.Message ?? "Riallineamento non riuscito.");
                }
                await transazione.CommitAsync();
                return ResponseBase<string>.Success($"Consuntivo {consuntivo.CrrgCSrl:0} aggiornato: {string.Join("; ", confronto.Sostanziali)}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Aggiornamento del consuntivo {CrrgCSrl} da Teamwork non riuscito", consuntivo.CrrgCSrl);
                await AnnullaAsync(transazione);
                return ResponseBase<string>.Failed("errore", $"Errore imprevisto: {ex.GetBaseException().Message}");
            }
        }

        // ---------------------------------------------------------------- Supporto

        private async Task<TeamworkConnessione?> GetConnessioneAsync()
        {
            var usr1CSrl = await _auth.GetUsr1CSrlCorrenteAsync();
            return usr1CSrl == null ? null : await _auth.GetConnessioneAsync(usr1CSrl.Value);
        }

        private string? Sigla() => string.IsNullOrWhiteSpace(_userScope.SV_USR_SIGLA) ? null : _userScope.SV_USR_SIGLA.Trim();

        private async Task<ResponseBase<TwTasksRisultato>> LeggiTaskAsync(TeamworkConnessione conn, List<long> taskIds)
        {
            if (taskIds.Count == 0)
            {
                return ResponseBase<TwTasksRisultato>.Success(new TwTasksRisultato { CampiPersonalizzatiPresenti = true });
            }
            return await _api.GetTasksAsync(conn, taskIds);
        }

        private async Task<Dictionary<string, TwCommessaMep>> RisolviCommesseAsync(IEnumerable<string?> valori)
        {
            var commesse = new Dictionary<string, TwCommessaMep>(StringComparer.OrdinalIgnoreCase);
            foreach (var valore in valori.Where(v => !string.IsNullOrWhiteSpace(v)).Select(v => v!.Trim()).Distinct(StringComparer.OrdinalIgnoreCase))
            {
                commesse[valore] = await RisolviCommessaAsync(valore);
            }
            return commesse;
        }

        /// <summary>ISL di MEP (flusso_tatv) con la sua commessa.</summary>
        private sealed record IslMep(string? Tst, string? Prf, decimal Anno, decimal Num, bool Offerta);

        /// <summary>Cerca l'ISL in MEP: null se non c'è, errore se il codice è presente più volte.</summary>
        private async Task<(IslMep? Isl, string? Errore)> CercaIslAsync(string codice)
        {
            var isl = await _db.FlussoTatvs.AsNoTracking()
                .Where(x => x.TatvRifCliente == codice)
                .Select(x => new IslMep(x.TatvTstComm, x.TatvPrfComm, x.TatvAComm, x.TatvNComm, x.TatvFlgOfferta > 0))
                .Take(2).ToListAsync();
            return isl.Count switch
            {
                0 => (null, null),
                1 => (isl[0], null),
                _ => (null, $"ISL {codice} presente più volte in MEP")
            };
        }

        /// <summary>
        /// Il campo Commessa/ISL del task può contenere:
        /// - il numero commessa (flusso_tbcp.tbcp_n_comm, es. 231009);
        /// - il codice ISL (flusso_tatv.tatv_rif_cliente): la commessa è quella della ISL;
        /// - "numerocommessa;ISL": vale l'ISL, il numero commessa deve essere quello della ISL.
        /// </summary>
        private async Task<TwCommessaMep> RisolviCommessaAsync(string valore)
        {
            string cDitta = CommonCostants.CDitta;
            var parti = valore.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (parti.Length == 0)
            {
                return new TwCommessaMep { Errore = "Campo Commessa/ISL del task vuoto" };
            }
            if (parti.Length > 2)
            {
                return new TwCommessaMep { Errore = $"«{valore.Trim()}»: formato non valido, usa «commessa», «ISL» oppure «commessa;ISL»" };
            }

            string? tst, prf; decimal anno, num; string? islTrovata = null; bool offerta = false;
            if (parti.Length == 2)
            {
                var (numero, codiceIsl) = (parti[0], parti[1]);
                if (!decimal.TryParse(numero, out var nAtteso))
                    return new TwCommessaMep { Errore = $"«{numero}» non è un numero commessa: il formato è «commessa;ISL»" };

                var (trovata, errore) = await CercaIslAsync(codiceIsl);
                if (errore != null)
                    return new TwCommessaMep { Isl = codiceIsl, Errore = errore };
                if (trovata == null)
                    return new TwCommessaMep { Errore = $"ISL {codiceIsl} non trovata in MEP" };
                if (trovata.Num != nAtteso)
                    return new TwCommessaMep { Isl = codiceIsl, Errore = $"L'ISL {codiceIsl} è della commessa {trovata.Num:0}, non della {numero}" };

                islTrovata = codiceIsl;
                offerta = trovata.Offerta;
                (tst, prf, anno, num) = (trovata.Tst, trovata.Prf, trovata.Anno, trovata.Num);
            }
            else
            {
                var v = parti[0];
                var (trovata, errore) = await CercaIslAsync(v);
                if (errore != null)
                    return new TwCommessaMep { Isl = v, Errore = errore };

                if (trovata != null)
                {
                    islTrovata = v;
                    offerta = trovata.Offerta;
                    (tst, prf, anno, num) = (trovata.Tst, trovata.Prf, trovata.Anno, trovata.Num);
                }
                else if (decimal.TryParse(v, out var nComm))
                {
                    var comm = await _db.FlussoTbcps.AsNoTracking()
                        .Where(x => x.TbcpNComm == nComm && x.TbcpCDitta == cDitta)
                        .Select(x => new { x.TbcpTstComm, x.TbcpPrfComm, x.TbcpAComm, x.TbcpNComm })
                        .Take(2).ToListAsync();
                    if (comm.Count == 0)
                        return new TwCommessaMep { Errore = $"Commessa {v} non trovata in MEP" };
                    if (comm.Count > 1)
                        return new TwCommessaMep { Errore = $"Numero commessa {v} ambiguo in MEP" };
                    (tst, prf, anno, num) = (comm[0].TbcpTstComm, comm[0].TbcpPrfComm, comm[0].TbcpAComm, comm[0].TbcpNComm);
                }
                else
                {
                    return new TwCommessaMep { Errore = $"«{v}» non è né una ISL né un numero commessa di MEP" };
                }
            }

            var desc = await _db.FlussoTbcps.AsNoTracking()
                .Where(x => x.TbcpTstComm == tst && x.TbcpPrfComm == prf && x.TbcpAComm == anno && x.TbcpNComm == num && x.TbcpCDitta == cDitta)
                .Select(x => x.TbcpDesc).FirstOrDefaultAsync();
            var aperta = await _db.VsPpCommAperteXClis.AsNoTracking()
                .AnyAsync(x => x.OrpbTstDoc == tst && x.OrpbPrfDoc == prf && x.OrpbADoc == anno && x.OrpbNDoc == num);

            return new TwCommessaMep
            {
                Isl = islTrovata,
                TstDoc = tst, PrfDoc = prf, ADoc = anno, NDoc = num,
                Descrizione = desc,
                Aperta = aperta,
                VerbaleObbligatorio = prf == "B" || (islTrovata != null && offerta),
                Errore = aperta ? null : "Commessa chiusa in MEP"
            };
        }

        /// <summary>Operazioni della commessa (riga 1), come nella tendina del form consuntivo. Valore "NOper-TOper".</summary>
        private async Task<List<TwOpzione>> GetOperazioniAsync(TwCommessaMep comm)
        {
            var righe = await (from o in _db.FlussoOlcas.AsNoTracking()
                               where o.OlcaTstDoc == comm.TstDoc && o.OlcaPrfDoc == comm.PrfDoc && o.OlcaADoc == comm.ADoc
                                     && o.OlcaNDoc == comm.NDoc && o.OlcaNRigaDoc == 1
                               join ci in _db.FlussoCitos.AsNoTracking()
                                   on new { D = o.OlcaCDitta, C = o.OlcaTOper } equals new { D = ci.CitoCDitta, C = (string?)ci.CitoCodice } into g
                               from ci in g.DefaultIfEmpty()
                               orderby o.OlcaNOper
                               select new { o.OlcaNOper, o.OlcaTOper, Desc = ci == null ? null : ci.CitoDescrizione })
                              .ToListAsync();

            return righe.Select(x => new TwOpzione
            {
                Valore = $"{x.OlcaNOper:0}-{x.OlcaTOper}",
                Testo = $"{x.OlcaNOper:0}-{x.OlcaTOper}{(string.IsNullOrWhiteSpace(x.Desc) ? "" : " - " + x.Desc)}"
            }).GroupBy(x => x.Valore).Select(g => g.First()).ToList();
        }

        private sealed class ConsuntivoManuale
        {
            public decimal CrrgCSrl { get; init; }
            public DateTime Data { get; init; }
            public string Codice { get; init; } = "";
            public int Secondi { get; init; }
        }

        private async Task<List<ConsuntivoManuale>> GetConsuntiviManualiAsync(string? sigla, DateTime dal, DateTime al)
        {
            if (string.IsNullOrEmpty(sigla))
            {
                return new List<ConsuntivoManuale>();
            }
            string cDitta = CommonCostants.CDitta;
            var ris = sigla.ToUpperInvariant();
            var da = dal.Date;
            var a = al.Date.AddDays(1);
            var righe = await _db.FlussoCrrgs.AsNoTracking()
                .Where(c => c.CrrgCRis == ris && c.CrrgCDitta == cDitta && c.CrrgDtt >= da && c.CrrgDtt < a
                            && !_db.FlussoCrrgTws.Any(t => t.CrrgCSrl == c.CrrgCSrl))
                .Select(c => new { c.CrrgCSrl, c.CrrgDtt, c.CrrgTstDoc, c.CrrgPrfDoc, c.CrrgADoc, c.CrrgNDoc, c.CrrgTmRunIncr })
                .ToListAsync();
            return righe.Select(c => new ConsuntivoManuale
            {
                CrrgCSrl = c.CrrgCSrl,
                Data = c.CrrgDtt?.Date ?? DateTime.MinValue,
                Codice = $"{c.CrrgTstDoc}-{c.CrrgPrfDoc}-{c.CrrgADoc:0}-{c.CrrgNDoc:0}",
                Secondi = (int)c.CrrgTmRunIncr
            }).ToList();
        }

        /// <summary>
        /// Note del consuntivo: "Nome del task - descrizione della registrazione", oppure solo il nome del task.
        /// Dal nome del task si toglie il prefisso "[commessa]" (es. "[231009] "), perché la commessa è già nel consuntivo.
        /// </summary>
        private static string ComponiNote(string? taskNome, string? descrizione)
        {
            var nome = System.Text.RegularExpressions.Regex.Replace(taskNome ?? "", @"^\s*\[[^\]]*\]\s*", "").Trim();
            var desc = descrizione?.Trim() ?? "";
            var note = desc.Length == 0 ? nome : nome.Length == 0 ? desc : $"{nome} - {desc}";
            return Tronca(note, MaxNote);
        }

        private static string Tronca(string s, int max) => s.Length <= max ? s : s[..max];

        private static string Durata(int minuti) => $"{minuti / 60}:{minuti % 60:00}";

        private static DateTime TroncaSecondi(DateTime d) => new DateTime(d.Ticks - d.Ticks % TimeSpan.TicksPerSecond, DateTimeKind.Utc);

        private static DateTime InOraLocale(DateTime utc) =>
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(utc, DateTimeKind.Utc), FusoItalia);

        private static DateTime DaOraLocale(DateTime locale) =>
            TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(locale, DateTimeKind.Unspecified), FusoItalia);

        private static TimeZoneInfo TrovaFusoItalia()
        {
            foreach (var id in new[] { "Europe/Rome", "W. Europe Standard Time" })
            {
                try { return TimeZoneInfo.FindSystemTimeZoneById(id); }
                catch (TimeZoneNotFoundException) { }
                catch (InvalidTimeZoneException) { }
            }
            return TimeZoneInfo.Local;
        }
    }
}
