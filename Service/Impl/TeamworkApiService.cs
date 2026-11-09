using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Mep01Web.DTO;
using MepWeb.Service.Interface;
using Microsoft.Extensions.Options;

namespace MepWeb.Service.Impl
{
    public class TeamworkApiService : ITeamworkApiService
    {
        private const int PageSize = 250;
        private const int MaxPagine = 40;
        private const int IdPerChiamata = 100;

        private readonly HttpClient _http;
        private readonly ITeamworkAuthService _auth;
        private readonly TeamworkOptions _options;
        private readonly ILogger<TeamworkApiService> _logger;

        public TeamworkApiService(HttpClient http, ITeamworkAuthService auth, IOptions<TeamworkOptions> options, ILogger<TeamworkApiService> logger)
        {
            _http = http;
            _auth = auth;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<ResponseBase<List<TwRegistrazione>>> GetRegistrazioniAsync(TeamworkConnessione conn, DateTime dal, DateTime al)
        {
            var query = $"startDate={dal:yyyy-MM-dd}&endDate={al:yyyy-MM-dd}&assignedToUserIds={conn.TwUserId}&orderBy=date&orderMode=asc";
            var esito = await LeggiRegistrazioniAsync(conn, query);
            if (!esito.Succeeded || esito.Body == null)
            {
                return esito;
            }
            // Per sicurezza si tengono solo le registrazioni dell'utente collegato
            return ResponseBase<List<TwRegistrazione>>.Success(
                esito.Body.Where(r => r.UserId == null || r.UserId == conn.TwUserId).ToList());
        }

        public async Task<ResponseBase<List<TwRegistrazione>>> GetRegistrazioniPerIdAsync(TeamworkConnessione conn, IEnumerable<long> ids)
        {
            var tutti = new List<TwRegistrazione>();
            foreach (var blocco in ids.Distinct().Chunk(IdPerChiamata))
            {
                var esito = await LeggiRegistrazioniAsync(conn, $"ids={string.Join(",", blocco)}&showDeleted=true");
                if (!esito.Succeeded || esito.Body == null)
                {
                    return esito;
                }
                tutti.AddRange(esito.Body);
            }
            return ResponseBase<List<TwRegistrazione>>.Success(tutti);
        }

        public async Task<ResponseBase<TwTasksRisultato>> GetTasksAsync(TeamworkConnessione conn, IEnumerable<long> ids)
        {
            var tasks = new Dictionary<long, TwTask>();
            var campiPresenti = true;
            var campo = _options.CampoCommessaTaskId;

            foreach (var blocco in ids.Distinct().Chunk(IdPerChiamata))
            {
                var url = $"{conn.ApiEndpoint}projects/api/v3/tasks.json?ids={string.Join(",", blocco)}" +
                          $"&includeCustomFields=true&includeCustomFieldIds={campo}&include=customfieldTasks" +
                          // Senza showDeleted: i task nel cestino non vengono restituiti e le loro registrazioni risultano cancellate
                          $"&includeCompletedTasks=true&includeArchivedProjects=true&pageSize={IdPerChiamata}";
                var risposta = await GetJsonAsync(conn, url);
                if (!risposta.Succeeded || risposta.Body == null)
                {
                    return ResponseBase<TwTasksRisultato>.Failed(risposta.Errors!);
                }

                using var json = risposta.Body;
                var root = json.RootElement;

                var valori = new Dictionary<long, string?>();
                var cft = TeamworkJson.Naviga(root, "included", "customfieldTasks");
                if (cft == null)
                {
                    campiPresenti = false;
                    _logger.LogWarning("Teamwork tasks.json senza included.customfieldTasks. Campi included: {Campi}",
                        TeamworkJson.Naviga(root, "included") is JsonElement inc ? TeamworkJson.ElencoCampi(inc) : "(nessuno)");
                }
                else
                {
                    foreach (var v in Elementi(cft.Value))
                    {
                        if (TeamworkJson.Long(v, "customfieldId") == campo)
                        {
                            var taskId = TeamworkJson.Long(v, "taskId") ?? TeamworkJson.Long(v, "task", "id");
                            if (taskId != null)
                            {
                                valori[taskId.Value] = TeamworkJson.Stringa(v, "value")?.Trim();
                            }
                        }
                    }
                }

                if (TeamworkJson.Naviga(root, "tasks") is JsonElement arr && arr.ValueKind == JsonValueKind.Array)
                {
                    foreach (var t in arr.EnumerateArray())
                    {
                        var id = TeamworkJson.Long(t, "id");
                        if (id == null) continue;
                        tasks[id.Value] = new TwTask
                        {
                            Id = id.Value,
                            Nome = TeamworkJson.Stringa(t, "name"),
                            Commessa = valori.TryGetValue(id.Value, out var c) && !string.IsNullOrWhiteSpace(c) ? c : null,
                            Cancellato = !string.IsNullOrEmpty(TeamworkJson.Stringa(t, "deletedAt"))
                                         || string.Equals(TeamworkJson.Stringa(t, "status"), "deleted", StringComparison.OrdinalIgnoreCase)
                        };
                    }
                }
            }

            return ResponseBase<TwTasksRisultato>.Success(new TwTasksRisultato { Tasks = tasks, CampiPersonalizzatiPresenti = campiPresenti });
        }

        /// <summary>Legge tutte le pagine di /projects/api/v3/time.json con la query indicata.</summary>
        private async Task<ResponseBase<List<TwRegistrazione>>> LeggiRegistrazioniAsync(TeamworkConnessione conn, string query)
        {
            var risultato = new List<TwRegistrazione>();
            for (var pagina = 1; pagina <= MaxPagine; pagina++)
            {
                var url = $"{conn.ApiEndpoint}projects/api/v3/time.json?{query}&pageSize={PageSize}&page={pagina}";
                var risposta = await GetJsonAsync(conn, url);
                if (!risposta.Succeeded || risposta.Body == null)
                {
                    return ResponseBase<List<TwRegistrazione>>.Failed(risposta.Errors!);
                }

                using var json = risposta.Body;
                var root = json.RootElement;
                if (TeamworkJson.Naviga(root, "timelogs") is JsonElement arr && arr.ValueKind == JsonValueKind.Array)
                {
                    foreach (var t in arr.EnumerateArray())
                    {
                        var id = TeamworkJson.Long(t, "id");
                        var quando = TeamworkJson.DataUtc(t, "timeLogged");
                        if (id == null || quando == null) continue;
                        risultato.Add(new TwRegistrazione
                        {
                            Id = id.Value,
                            UserId = TeamworkJson.Long(t, "userId") ?? TeamworkJson.Long(t, "user", "id"),
                            TaskId = NullSeZero(TeamworkJson.Long(t, "taskId") ?? TeamworkJson.Long(t, "task", "id")),
                            ProjectId = TeamworkJson.Long(t, "projectId") ?? TeamworkJson.Long(t, "project", "id"),
                            Minuti = (int)(TeamworkJson.Long(t, "minutes") ?? 0),
                            TimeLoggedUtc = quando.Value,
                            Descrizione = TeamworkJson.Stringa(t, "description"),
                            UpdatedAtUtc = TeamworkJson.DataUtc(t, "updatedAt") ?? TeamworkJson.DataUtc(t, "dateEdited"),
                            Cancellata = TeamworkJson.Bool(t, "deleted")
                        });
                    }
                }
                else
                {
                    _logger.LogWarning("Teamwork time.json senza 'timelogs'. Campi: {Campi}", TeamworkJson.ElencoCampi(root));
                }

                if (!TeamworkJson.Bool(root, "meta", "page", "hasMore"))
                {
                    break;
                }
            }
            return ResponseBase<List<TwRegistrazione>>.Success(risultato);
        }

        /// <summary>GET autenticato; su 401 elimina il collegamento dell'utente.</summary>
        private async Task<ResponseBase<JsonDocument>> GetJsonAsync(TeamworkConnessione conn, string url)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", conn.AccessToken);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage resp;
            try
            {
                resp = await _http.SendAsync(req);
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                _logger.LogWarning(ex, "Chiamata Teamwork non riuscita: {Url}", url);
                return ResponseBase<JsonDocument>.Failed("rete", $"Teamwork non raggiungibile: {ex.Message}");
            }

            using (resp)
            {
                if (resp.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await _auth.ScollegaAsync(conn.Usr1CSrl);
                    return ResponseBase<JsonDocument>.Failed("401", "Il collegamento Teamwork non è più valido: ricollega l'account dal menu.");
                }
                var body = await resp.Content.ReadAsStringAsync();
                if (!resp.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Teamwork {Status} su {Url}: {Body}", (int)resp.StatusCode, url, body.Length > 500 ? body[..500] : body);
                    return ResponseBase<JsonDocument>.Failed(((int)resp.StatusCode).ToString(),
                        $"Teamwork ha risposto {(int)resp.StatusCode}: {(body.Length > 300 ? body[..300] + "…" : body)}");
                }
                return ResponseBase<JsonDocument>.Success(JsonDocument.Parse(body));
            }
        }

        /// <summary>Gli "included" di Teamwork sono oggetti indicizzati per id; per sicurezza si accettano anche array.</summary>
        private static IEnumerable<JsonElement> Elementi(JsonElement e) =>
            e.ValueKind switch
            {
                JsonValueKind.Object => e.EnumerateObject().Select(p => p.Value),
                JsonValueKind.Array => e.EnumerateArray(),
                _ => Enumerable.Empty<JsonElement>()
            };

        private static long? NullSeZero(long? v) => v is null or 0 ? null : v;
    }
}
