using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MepWeb.Service;
using Microsoft.AspNetCore.Authentication;

namespace MepWeb.Controllers
{
    public class SessionTimeoutFilterAttribute : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var path = context.HttpContext.Request.Path.Value;

            // Escludi solo le rotte del BuildController e la pagina di login dal controllo di sessione
            if (path != null &&
                !path.EndsWith("/login/login", StringComparison.InvariantCultureIgnoreCase) &&
                !path.StartsWith("/api/build/", StringComparison.InvariantCultureIgnoreCase) &&
                string.IsNullOrWhiteSpace(context.HttpContext.Session.GetString("SV_USR_EMAIL")))
            {
                await context.HttpContext.SignOutAsync();
                // Esegui il logout o effettua altra gestione in caso di sessione scaduta
                context.Result = new RedirectToActionResult("Login", "Login", null);
            }
            else
            {
                await next();
            }
        }
    }
}
