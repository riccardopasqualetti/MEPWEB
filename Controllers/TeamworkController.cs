using System.Security.Cryptography;
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
        private readonly UserScope _userScope;
        private readonly TeamworkOptions _options;

        public TeamworkController(ITeamworkAuthService auth, UserScope userScope, IOptions<TeamworkOptions> options)
        {
            _auth = auth;
            _userScope = userScope;
            _options = options.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Configurato = _auth.IsConfigurato;
            ViewBag.RedirectUri = GetRedirectUri();
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
