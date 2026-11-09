using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using MepWeb.Costants;
using MepWeb.DTO.Response;
using MepWeb.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MepWeb.Service.Impl
{
    public class TeamworkImportService : ITeamworkImportService
    {
        private static readonly TimeZoneInfo FusoItalia = TrovaFusoItalia();

        private readonly SataconsultingContext _db;
        private readonly ITeamworkAuthService _auth;
        private readonly ITeamworkApiService _api;
        private readonly ICrrgTwService _crrgTw;
        private readonly UserScope _userScope;

        public TeamworkImportService(SataconsultingContext db, ITeamworkAuthService auth, ITeamworkApiService api,
            ICrrgTwService crrgTw, UserScope userScope)
        {
            _db = db;
            _auth = auth;
            _api = api;
            _crrgTw = crrgTw;
            _userScope = userScope;
        }

        public async Task<ResponseBase<TwAnteprimaResponse>> GetAnteprimaAsync(DateTime dal, DateTime al)
        {
            var usr1CSrl = await _auth.GetUsr1CSrlCorrenteAsync();
            var conn = usr1CSrl == null ? null : await _auth.GetConnessioneAsync(usr1CSrl.Value);
            if (conn == null)
            {
                return ResponseBase<TwAnteprimaResponse>.Failed("nonCollegato", "Account Teamwork non collegato: collegalo dal menu in alto a destra.");
            }
            var sigla = _userScope.SV_USR_SIGLA;
            var risultato = new TwAnteprimaResponse { Dal = dal.Date, Al = al.Date };

            // 1. Registrazioni del periodo su Teamwork
            var regEsito = await _api.GetRegistrazioniAsync(conn, dal, al);
            if (!regEsito.Succeeded || regEsito.Body == null)
            {
                return ResponseBase<TwAnteprimaResponse>.Failed(regEsito.Errors!);
            }
            var registrazioni = regEsito.Body.Where(r => !r.Cancellata).ToDictionary(r => r.Id);

            // 2. Collegamenti: quelli delle registrazioni lette + quelli importati da me con data nel periodo
            var dalUtc = TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(dal.Date, DateTimeKind.Unspecified), FusoItalia);
            var alUtc = TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(al.Date.AddDays(1), DateTimeKind.Unspecified), FusoItalia).AddSeconds(-1);
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
            var tasks = new Dictionary<long, TwTask>();
            if (taskIds.Count > 0)
            {
                var taskEsito = await _api.GetTasksAsync(conn, taskIds);
                if (!taskEsito.Succeeded || taskEsito.Body == null)
                {
                    return ResponseBase<TwAnteprimaResponse>.Failed(taskEsito.Errors!);
                }
                tasks = taskEsito.Body.Tasks;
                if (!taskEsito.Body.CampiPersonalizzatiPresenti)
                {
                    risultato.Avvisi.Add("Teamwork non ha restituito i campi personalizzati dei task: il campo Commessa/ISL risulta vuoto per tutte le righe.");
                }
            }

            // 5. Commesse MEP per ogni valore distinto del campo Commessa/ISL
            var commesse = new Dictionary<string, TwCommessaMep>(StringComparer.OrdinalIgnoreCase);
            foreach (var valore in tasks.Values.Select(t => t.Commessa).Where(v => !string.IsNullOrWhiteSpace(v)).Distinct(StringComparer.OrdinalIgnoreCase))
            {
                commesse[valore!] = await RisolviCommessaAsync(valore!);
            }

            // 6. Consuntivi ancora esistenti
            var srlCollegati = collegamenti.Values.Select(c => c.CrrgCSrl).Distinct().ToList();
            string cDitta = CommonCostants.CDitta;
            var consuntiviEsistenti = srlCollegati.Count == 0
                ? new HashSet<decimal>()
                : (await _db.FlussoCrrgs.AsNoTracking()
                    .Where(c => srlCollegati.Contains(c.CrrgCSrl) && c.CrrgCDitta == cDitta)
                    .Select(c => c.CrrgCSrl).ToListAsync()).ToHashSet();

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

            return ResponseBase<TwAnteprimaResponse>.Success(risultato);
        }

        private void AggiungiCollegata(TwAnteprimaResponse risultato, TwRegistrazione reg, TwTask? task, FlussoCrrgTw coll,
            Dictionary<string, TwCommessaMep> commesse, HashSet<decimal> consuntiviEsistenti)
        {
            if (!consuntiviEsistenti.Contains(coll.CrrgCSrl))
            {
                risultato.DaAllineare.Add(CreaRiga(reg, task, commesse, TwStatoRegistrazione.ConsuntivoEliminatoInMep, coll.CrrgCSrl,
                    new List<string> { $"Il consuntivo {coll.CrrgCSrl:0} non esiste più in MEP" }));
                return;
            }

            var diff = new List<string>();
            if (coll.TwMinutes != reg.Minuti)
                diff.Add($"Ore: {Durata(coll.TwMinutes)} → {Durata(reg.Minuti)}");
            if (TroncaSecondi(coll.TwDate) != TroncaSecondi(reg.TimeLoggedUtc))
                diff.Add($"Data: {InOraLocale(coll.TwDate):dd/MM/yyyy HH:mm} → {InOraLocale(reg.TimeLoggedUtc):dd/MM/yyyy HH:mm}");
            if ((coll.TwDescrizioneReg ?? "") != (reg.Descrizione ?? ""))
                diff.Add("Descrizione modificata");
            if (!string.Equals(coll.TwCommessa ?? "", task?.Commessa ?? "", StringComparison.OrdinalIgnoreCase))
                diff.Add($"Commessa/ISL del task: {coll.TwCommessa ?? "(vuoto)"} → {task?.Commessa ?? "(vuoto)"}");

            var aggiornataSuTeamwork = reg.UpdatedAtUtc != null && TroncaSecondi(reg.UpdatedAtUtc.Value) != TroncaSecondi(coll.TwUpdatedAt);
            if (diff.Count == 0 && aggiornataSuTeamwork)
                diff.Add("Modificati solo dati secondari (es. fatturabile): basta riallineare la data di aggiornamento");

            var stato = diff.Count == 0 ? TwStatoRegistrazione.Importata : TwStatoRegistrazione.ModificataInTeamwork;
            var riga = CreaRiga(reg, task, commesse, stato, coll.CrrgCSrl, diff);
            (stato == TwStatoRegistrazione.Importata ? risultato.Importate : risultato.DaAllineare).Add(riga);
        }

        private static TwRigaAnteprima CreaRiga(TwRegistrazione reg, TwTask? task, Dictionary<string, TwCommessaMep> commesse,
            TwStatoRegistrazione stato, decimal? crrgCSrl, List<string> differenze, string? commessaTeamwork = null)
        {
            var valore = commessaTeamwork ?? task?.Commessa;
            TwCommessaMep? commessa = !string.IsNullOrWhiteSpace(valore) && commesse.TryGetValue(valore, out var c) ? c : null;

            string? nonImportabile = null;
            if (reg.TaskId == null)
                nonImportabile = "Registrazione senza task: manca il campo Commessa/ISL";
            else if (task == null)
                nonImportabile = "Task non leggibile su Teamwork";
            else if (string.IsNullOrWhiteSpace(valore))
                nonImportabile = "Campo Commessa/ISL del task vuoto";
            else if (commessa?.Errore != null)
                nonImportabile = commessa.Errore;

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
                NonImportabile = nonImportabile
            };
        }

        /// <summary>
        /// Il campo Commessa/ISL può contenere un codice ISL (flusso_tatv.tatv_rif_cliente) o il numero commessa
        /// (flusso_tbcp.tbcp_n_comm, es. 231009). Con una ISL la commessa è quella della ISL.
        /// </summary>
        private async Task<TwCommessaMep> RisolviCommessaAsync(string valore)
        {
            string cDitta = CommonCostants.CDitta;
            var v = valore.Trim();

            var isl = await _db.FlussoTatvs.AsNoTracking()
                .Where(x => x.TatvRifCliente == v)
                .Select(x => new { x.TatvTstComm, x.TatvPrfComm, x.TatvAComm, x.TatvNComm })
                .Take(2).ToListAsync();
            if (isl.Count > 1)
            {
                return new TwCommessaMep { Isl = v, Errore = $"ISL {v} presente più volte in MEP" };
            }

            string? tst, prf; decimal anno, num; string? islTrovata = null;
            if (isl.Count == 1)
            {
                islTrovata = v;
                (tst, prf, anno, num) = (isl[0].TatvTstComm, isl[0].TatvPrfComm, isl[0].TatvAComm, isl[0].TatvNComm);
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
                Errore = aperta ? null : "Commessa chiusa in MEP"
            };
        }

        private static string Durata(int minuti) => $"{minuti / 60}:{minuti % 60:00}";

        private static DateTime TroncaSecondi(DateTime d) => new DateTime(d.Ticks - d.Ticks % TimeSpan.TicksPerSecond, DateTimeKind.Utc);

        private static DateTime InOraLocale(DateTime utc) =>
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(utc, DateTimeKind.Utc), FusoItalia);

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
