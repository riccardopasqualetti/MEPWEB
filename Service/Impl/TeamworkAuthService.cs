using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;
using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using MepWeb.Costants;
using MepWeb.Service.Interface;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MepWeb.Service.Impl
{
    public class TeamworkAuthService : ITeamworkAuthService
    {
        private readonly HttpClient _http;
        private readonly SataconsultingContext _db;
        private readonly TeamworkOptions _options;
        private readonly IDataProtector _protector;
        private readonly ILogger<TeamworkAuthService> _logger;
        private readonly UserScope _userScope;

        public TeamworkAuthService(HttpClient http, SataconsultingContext db, IOptions<TeamworkOptions> options,
            IDataProtectionProvider dataProtectionProvider, ILogger<TeamworkAuthService> logger, UserScope userScope)
        {
            _http = http;
            _db = db;
            _options = options.Value;
            _protector = dataProtectionProvider.CreateProtector("MepWeb.Teamwork.AccessToken.v1");
            _logger = logger;
            _userScope = userScope;
        }

        public bool IsConfigurato =>
            !string.IsNullOrWhiteSpace(_options.ClientId) && !string.IsNullOrWhiteSpace(_options.ClientSecret);

        public async Task<decimal?> GetUsr1CSrlCorrenteAsync()
        {
            if (decimal.TryParse(_userScope.SV_USR_C_SRL, NumberStyles.Number, CultureInfo.InvariantCulture, out var inSessione))
            {
                return inSessione;
            }

            var email = _userScope.SV_USR_EMAIL;
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            string cDitta = CommonCostants.CDitta;
            var srl = await _db.FlussoUsr1s.AsNoTracking()
                .Where(u => u.Usr1EMail == email && u.Usr1CDitta == cDitta)
                .Select(u => (decimal?)u.Usr1CSrl)
                .FirstOrDefaultAsync();

            if (srl != null)
            {
                _userScope.SV_USR_C_SRL = srl.Value.ToString(CultureInfo.InvariantCulture);
            }
            return srl;
        }

        public string GetLoginUrl(string redirectUri, string state)
        {
            return $"{_options.LoginUrl}?client_id={Uri.EscapeDataString(_options.ClientId)}" +
                   $"&redirect_uri={Uri.EscapeDataString(redirectUri)}&state={Uri.EscapeDataString(state)}";
        }

        public async Task<ResponseBase> CompletaLoginAsync(string code, string redirectUri, decimal usr1CSrl)
        {
            if (!IsConfigurato)
            {
                return ResponseBase.Failed("config", "Login Teamwork non configurato: manca ClientId o ClientSecret.");
            }

            string cDitta = CommonCostants.CDitta;
            var emailMepweb = await _db.FlussoUsr1s.AsNoTracking()
                .Where(u => u.Usr1CSrl == usr1CSrl && u.Usr1CDitta == cDitta)
                .Select(u => u.Usr1EMail)
                .FirstOrDefaultAsync();
            if (string.IsNullOrWhiteSpace(emailMepweb))
            {
                return ResponseBase.Failed("utente", "Utente MEPWEB senza email: impossibile verificare l'account Teamwork.");
            }

            // 1. code -> access token
            using var tokenResp = await _http.PostAsJsonAsync(_options.TokenUrl, new
            {
                code,
                client_id = _options.ClientId,
                client_secret = _options.ClientSecret,
                redirect_uri = redirectUri
            });
            var tokenBody = await tokenResp.Content.ReadAsStringAsync();
            if (!tokenResp.IsSuccessStatusCode)
            {
                _logger.LogWarning("Teamwork token.json ha risposto {Status}: {Body}", (int)tokenResp.StatusCode, Tronca(tokenBody));
                return ResponseBase.Failed("token", $"Teamwork ha rifiutato il login ({(int)tokenResp.StatusCode}): {Tronca(tokenBody)}");
            }

            using var tokenJson = JsonDocument.Parse(tokenBody);
            var root = tokenJson.RootElement;
            var accessToken = LeggiStringa(root, "access_token");
            var apiEndpoint = LeggiStringa(root, "installation", "apiEndPoint") ?? LeggiStringa(root, "installation", "url");
            var twUserId = LeggiLong(root, "user", "id");
            var twEmail = LeggiStringa(root, "user", "email");

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(apiEndpoint))
            {
                _logger.LogWarning("Risposta token.json senza access_token o apiEndPoint. Campi: {Campi}", ElencoCampi(root));
                return ResponseBase.Failed("token", "Risposta di Teamwork non riconosciuta: mancano access_token o apiEndPoint.");
            }

            // 2. Se la risposta del token non contiene l'utente, lo si chiede a userinfo.json
            if (twUserId == null || string.IsNullOrWhiteSpace(twEmail))
            {
                using var req = new HttpRequestMessage(HttpMethod.Get, _options.UserInfoUrl);
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using var userResp = await _http.SendAsync(req);
                var userBody = await userResp.Content.ReadAsStringAsync();
                if (!userResp.IsSuccessStatusCode)
                {
                    return ResponseBase.Failed("userinfo", $"Impossibile leggere l'utente Teamwork ({(int)userResp.StatusCode}).");
                }
                using var userJson = JsonDocument.Parse(userBody);
                var u = userJson.RootElement;
                twUserId ??= LeggiLong(u, "user", "id") ?? LeggiLong(u, "id");
                twEmail = LeggiStringa(u, "user", "email") ?? LeggiStringa(u, "email") ?? twEmail;
                if (twUserId == null || string.IsNullOrWhiteSpace(twEmail))
                {
                    _logger.LogWarning("Risposta userinfo.json senza id o email utente. Campi: {Campi}", ElencoCampi(u));
                    return ResponseBase.Failed("userinfo", "Risposta di Teamwork non riconosciuta: mancano id o email dell'utente.");
                }
            }

            // 3. L'account Teamwork deve essere quello dell'utente MEPWEB collegato
            if (!string.Equals(twEmail.Trim(), emailMepweb.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return ResponseBase.Failed("email",
                    $"L'account Teamwork ({twEmail}) non corrisponde al tuo utente MEPWEB ({emailMepweb}). Esci da Teamwork e accedi con il tuo account.");
            }

            // 4. Salvataggio con token cifrato
            var adesso = DateTime.Now;
            var record = await _db.FlussoUsr1Tws.SingleOrDefaultAsync(x => x.Usr1CSrl == usr1CSrl);
            if (record == null)
            {
                record = new FlussoUsr1Tw { Usr1CSrl = usr1CSrl, DtIns = adesso };
                _db.FlussoUsr1Tws.Add(record);
            }
            record.TwUserId = twUserId.Value;
            record.TwApiEndpoint = ConBarraFinale(apiEndpoint);
            record.TwAccessToken = _protector.Protect(accessToken);
            record.DtUm = adesso;
            await _db.SaveChangesAsync();
            AggiornaStatoMenu(usr1CSrl, true);

            return new ResponseBase(true);
        }

        public async Task<FlussoUsr1Tw?> GetCollegamentoAsync(decimal usr1CSrl)
        {
            return await _db.FlussoUsr1Tws.AsNoTracking().SingleOrDefaultAsync(x => x.Usr1CSrl == usr1CSrl);
        }

        public async Task<TeamworkConnessione?> GetConnessioneAsync(decimal usr1CSrl)
        {
            var record = await GetCollegamentoAsync(usr1CSrl);
            if (record == null)
            {
                return null;
            }

            try
            {
                return new TeamworkConnessione
                {
                    Usr1CSrl = record.Usr1CSrl,
                    TwUserId = record.TwUserId,
                    ApiEndpoint = ConBarraFinale(record.TwApiEndpoint),
                    AccessToken = _protector.Unprotect(record.TwAccessToken)
                };
            }
            catch (CryptographicException)
            {
                // Chiavi di Data Protection cambiate (es. altro server): il token non è più leggibile, serve un nuovo login.
                _logger.LogWarning("Token Teamwork dell'utente {Usr1CSrl} non decifrabile: richiesto nuovo login", usr1CSrl);
                await ScollegaAsync(usr1CSrl);
                return null;
            }
        }

        public async Task ScollegaAsync(decimal usr1CSrl)
        {
            var record = await _db.FlussoUsr1Tws.SingleOrDefaultAsync(x => x.Usr1CSrl == usr1CSrl);
            if (record != null)
            {
                _db.FlussoUsr1Tws.Remove(record);
                await _db.SaveChangesAsync();
            }
            AggiornaStatoMenu(usr1CSrl, false);
        }

        public async Task<ResponseBase<string>> VerificaAsync(decimal usr1CSrl)
        {
            var conn = await GetConnessioneAsync(usr1CSrl);
            if (conn == null)
            {
                return ResponseBase<string>.Failed("nonCollegato", "Account Teamwork non collegato.");
            }

            using var req = new HttpRequestMessage(HttpMethod.Get, conn.ApiEndpoint + "projects/api/v3/me.json");
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", conn.AccessToken);
            using var resp = await _http.SendAsync(req);

            if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await ScollegaAsync(usr1CSrl);
                return ResponseBase<string>.Failed("401", "Il collegamento Teamwork non è più valido (password cambiata o accesso revocato): ricollega l'account.");
            }
            var body = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode)
            {
                return ResponseBase<string>.Failed(((int)resp.StatusCode).ToString(), $"Teamwork ha risposto {(int)resp.StatusCode}: {Tronca(body)}");
            }

            using var json = JsonDocument.Parse(body);
            var p = json.RootElement;
            var nome = $"{LeggiStringa(p, "person", "firstName")} {LeggiStringa(p, "person", "lastName")}".Trim();
            var email = LeggiStringa(p, "person", "email");
            var id = LeggiLong(p, "person", "id");
            return ResponseBase<string>.Success($"{nome} ({email}), id Teamwork {id}");
        }

        /// <summary>Aggiorna lo stato mostrato nel menu, se l'operazione riguarda l'utente collegato.</summary>
        private void AggiornaStatoMenu(decimal usr1CSrl, bool collegato)
        {
            if (decimal.TryParse(_userScope.SV_USR_C_SRL, NumberStyles.Number, CultureInfo.InvariantCulture, out var corrente)
                && corrente == usr1CSrl)
            {
                _userScope.SV_TW_COLLEGATO = collegato ? "S" : "N";
            }
        }

        private static string ConBarraFinale(string url) => url.EndsWith("/") ? url : url + "/";

        private static string Tronca(string s) => s.Length <= 300 ? s : s[..300] + "…";

        private static string ElencoCampi(JsonElement e) =>
            e.ValueKind == JsonValueKind.Object ? string.Join(", ", e.EnumerateObject().Select(p => p.Name)) : e.ValueKind.ToString();

        private static JsonElement? Naviga(JsonElement e, string[] percorso)
        {
            foreach (var nome in percorso)
            {
                if (e.ValueKind != JsonValueKind.Object || !e.TryGetProperty(nome, out var figlio))
                {
                    return null;
                }
                e = figlio;
            }
            return e;
        }

        private static string? LeggiStringa(JsonElement e, params string[] percorso)
        {
            var v = Naviga(e, percorso);
            return v?.ValueKind switch
            {
                JsonValueKind.String => v.Value.GetString(),
                JsonValueKind.Number => v.Value.GetRawText(),
                _ => null
            };
        }

        private static long? LeggiLong(JsonElement e, params string[] percorso)
        {
            var v = Naviga(e, percorso);
            if (v?.ValueKind == JsonValueKind.Number && v.Value.TryGetInt64(out var n))
            {
                return n;
            }
            if (v?.ValueKind == JsonValueKind.String && long.TryParse(v.Value.GetString(), out var s))
            {
                return s;
            }
            return null;
        }
    }
}
