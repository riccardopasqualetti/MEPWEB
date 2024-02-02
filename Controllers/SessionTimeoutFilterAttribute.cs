using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MepWeb.Service;

namespace MepWeb.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SessionTimeoutFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrWhiteSpace(context.HttpContext.Session.GetString("SV_USR_EMAIL")))
            {
                // Esegui il logout o effettua altra gestione in caso di sessione scaduta
                context.Result = new RedirectToActionResult("LogoutGet", "Login", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
