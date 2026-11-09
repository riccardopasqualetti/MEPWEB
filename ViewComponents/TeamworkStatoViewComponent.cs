using MepWeb.Service;
using MepWeb.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MepWeb.ViewComponents
{
    /// <summary>
    /// [231009] Pulsante Teamwork nel menu: mostra se l'utente ha collegato il proprio account.
    /// Lo stato si legge dal database una volta per sessione e poi resta in sessione (SV_TW_COLLEGATO),
    /// aggiornato da TeamworkAuthService quando l'utente collega o scollega l'account.
    /// </summary>
    public class TeamworkStatoViewComponent : ViewComponent
    {
        private readonly ITeamworkAuthService _auth;
        private readonly UserScope _userScope;

        public TeamworkStatoViewComponent(ITeamworkAuthService auth, UserScope userScope)
        {
            _auth = auth;
            _userScope = userScope;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_userScope.SV_TW_COLLEGATO == null)
            {
                var usr1CSrl = await _auth.GetUsr1CSrlCorrenteAsync();
                if (usr1CSrl == null)
                {
                    return Content(string.Empty);
                }
                var collegamento = await _auth.GetCollegamentoAsync(usr1CSrl.Value);
                _userScope.SV_TW_COLLEGATO = collegamento != null ? "S" : "N";
            }

            return View(_userScope.SV_TW_COLLEGATO == "S");
        }
    }
}
