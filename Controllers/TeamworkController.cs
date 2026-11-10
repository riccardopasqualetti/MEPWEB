using System.Security.Cryptography;
using MepWeb.DTO.Response;
using MepWeb.Service;
using MepWeb.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MepWeb.Controllers
{
    /// <summary>
    /// [231009] Collegamento dell'account Teamwork dell'utente MEPWEB (login OAuth per utente).
    ///
    /// Flusso: Collega -> login su Teamwork -> Callback (da teamwork.com, senza cookie) -> Completa (con sessione) -> Index.
    /// </summary>
    [Authorize]
    public class TeamworkController : Controller
    {
        private const string SessionState = "TW_OAUTH_STATE";

        private readonly ITeamworkAuthService _auth;
        private readonly ITeamworkImportService _import;
        private readonly UserScope _userScope;
        private readonly TeamworkOptions _options;

        public TeamworkController(ITeamworkAuthService auth, ITeamworkImportService import, UserScope userScope, IOptions<TeamworkOptions> options)
        {
            _auth = auth;
            _import = import;
            _userScope = userScope;
            _options = options.Value;
        }

        /// <summary>
        /// Pagina di import: registrazioni Teamwork del periodo con il loro stato.
        /// Periodo predefinito: dal lunedì della settimana corrente a oggi.
        /// Dopo il primo caricamento la pagina lavora via fetch (TeamworkImport.js): Griglie, Importa e Allinea.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Registrazioni(DateTime? dal, DateTime? al)
        {
            var (inizio, fine) = Periodo(dal, al);
            var esito = await _import.GetAnteprimaAsync(inizio, fine);
            if (!esito.Succeeded || esito.Body == null)
            {
                var errore = esito.Errors?.FirstOrDefault();
                if (errore?.Code is "nonCollegato" or "401")
                {
                    TempData["TwErrore"] = errore.Message;
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Errore"] = errore?.Message ?? "Lettura da Teamwork non riuscita.";
                return View(new TwAnteprimaResponse { Dal = inizio, Al = fine });
            }
            return View(esito.Body);
        }

        /// <summary>Solo le griglie (partial), per aggiornare la pagina senza ricaricarla.</summary>
        [HttpGet]
        public async Task<IActionResult> Griglie(DateTime? dal, DateTime? al)
        {
            var (inizio, fine) = Periodo(dal, al);
            var esito = await _import.GetAnteprimaAsync(inizio, fine);
            if (!esito.Succeeded || esito.Body == null)
            {
                var errore = esito.Errors?.FirstOrDefault();
                ViewData["Errore"] = errore?.Message ?? "Lettura da Teamwork non riuscita.";
                ViewData["NonCollegato"] = errore?.Code is "nonCollegato" or "401";
                return PartialView("_Griglie", new TwAnteprimaResponse { Dal = inizio, Al = fine });
            }
            return PartialView("_Griglie", esito.Body);
        }

        /// <summary>Azione della griglia "Da allineare" (aggiorna consuntivo, scollega, elimina consuntivo). Risposta JSON.</summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Allinea(long timelogId, TwAzioneAllineamento azione)
        {
            var esito = await _import.AllineaAsync(timelogId, azione);
            return Json(new
            {
                ok = esito.Succeeded,
                messaggio = esito.Succeeded ? esito.Body : esito.Errors?.FirstOrDefault()?.Message ?? "Operazione non riuscita."
            });
        }

        /// <summary>Importa come consuntivi le registrazioni selezionate. Risposta JSON con consuntivi creati ed errori.</summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Importa(MepWeb.DTO.Request.TwImportRequest request)
        {
            var esito = await _import.ImportaAsync(request);
            if (!esito.Succeeded || esito.Body == null)
            {
                return Json(new { errore = esito.Errors?.FirstOrDefault()?.Message ?? "Import non riuscito." });
            }
            return Json(new { importate = esito.Body.Importate, errori = esito.Body.Errori });
        }

        /// <summary>Periodo richiesto, con estremi in ordine e al massimo 3 mesi.</summary>
        private static (DateTime inizio, DateTime fine) Periodo(DateTime? dal, DateTime? al)
        {
            var oggi = DateTime.Today;
            var inizio = (dal ?? oggi.AddDays(-(((int)oggi.DayOfWeek + 6) % 7))).Date;
            var fine = (al ?? oggi).Date;
            if (fine < inizio)
            {
                (inizio, fine) = (fine, inizio);
            }
            if ((fine - inizio).TotalDays > 92)
            {
                fine = inizio.AddDays(92);
            }
            return (inizio, fine);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Configurato = _auth.IsConfigurato;
            ViewBag.EmailMepweb = _userScope.SV_USR_EMAIL;

            var usr1CSrl = await _auth.GetUsr1CSrlCorrenteAsync();
            if (usr1CSrl == null)
            {
                ViewBag.Configurato = false;
                TempData["TwErrore"] = "Utente MEPWEB non trovato in flusso_usr1: impossibile collegare Teamwork.";
                return View((Mep01Web.Models.FlussoUsr1Tw?)null);
            }
            var collegamento = await _auth.GetCollegamentoAsync(usr1CSrl.Value);
            return View(collegamento);
        }

        [HttpGet]
        public IActionResult Collega()
        {
            if (!_auth.IsConfigurato)
            {
                TempData["TwErrore"] = "Login Teamwork non configurato: manca ClientId o ClientSecret.";
                return RedirectToAction(nameof(Index));
            }

            var state = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
            HttpContext.Session.SetString(SessionState, state);
            return Redirect(_auth.GetLoginUrl(GetRedirectUri(), state));
        }

        /// <summary>
        /// Teamwork rimanda qui dopo il login. È una navigazione da un altro sito, quindi i cookie SameSite=Strict
        /// (login e sessione) non arrivano: non si fa nessun controllo qui, si rimanda subito a Completa dallo
        /// stesso sito, così il browser invia i cookie.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Callback(string? code, string? state, string? error)
        {
            ViewBag.UrlCompleta = Url.Action(nameof(Completa), new { code, state, error });
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Completa(string? code, string? state, string? error)
        {
            var stateAtteso = HttpContext.Session.GetString(SessionState);
            HttpContext.Session.Remove(SessionState);

            if (!string.IsNullOrEmpty(error))
            {
                TempData["TwErrore"] = $"Login Teamwork annullato o non riuscito: {error}";
                return RedirectToAction(nameof(Index));
            }
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state) || stateAtteso == null
                || !CryptographicOperations.FixedTimeEquals(System.Text.Encoding.ASCII.GetBytes(state), System.Text.Encoding.ASCII.GetBytes(stateAtteso)))
            {
                TempData["TwErrore"] = "Risposta di Teamwork non valida o scaduta: riprova il collegamento.";
                return RedirectToAction(nameof(Index));
            }

            var usr1CSrl = await _auth.GetUsr1CSrlCorrenteAsync();
            if (usr1CSrl == null)
            {
                TempData["TwErrore"] = "Utente MEPWEB non trovato in flusso_usr1.";
                return RedirectToAction(nameof(Index));
            }

            var esito = await _auth.CompletaLoginAsync(code, GetRedirectUri(), usr1CSrl.Value);
            if (!esito.Succeeded)
            {
                TempData["TwErrore"] = esito.Errors?.FirstOrDefault()?.Message ?? "Collegamento non riuscito.";
            }
            else
            {
                TempData["TwOk"] = "Account Teamwork collegato.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Scollega()
        {
            var usr1CSrl = await _auth.GetUsr1CSrlCorrenteAsync();
            if (usr1CSrl != null)
            {
                await _auth.ScollegaAsync(usr1CSrl.Value);
            }
            TempData["TwOk"] = "Account Teamwork scollegato.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verifica()
        {
            var usr1CSrl = await _auth.GetUsr1CSrlCorrenteAsync();
            if (usr1CSrl == null)
            {
                TempData["TwErrore"] = "Utente MEPWEB non trovato in flusso_usr1.";
                return RedirectToAction(nameof(Index));
            }

            var esito = await _auth.VerificaAsync(usr1CSrl.Value);
            if (esito.Succeeded)
            {
                TempData["TwOk"] = $"Teamwork risponde: collegato come {esito.Body}.";
            }
            else
            {
                TempData["TwErrore"] = esito.Errors?.FirstOrDefault()?.Message ?? "Verifica non riuscita.";
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>Indirizzo di callback: da configurazione se impostato, altrimenti calcolato dalla richiesta corrente.</summary>
        private string GetRedirectUri()
        {
            if (!string.IsNullOrWhiteSpace(_options.RedirectUri))
            {
                return _options.RedirectUri;
            }
            return Url.Action(nameof(Callback), "Teamwork", null, Request.Scheme)!;
        }
    }
}
