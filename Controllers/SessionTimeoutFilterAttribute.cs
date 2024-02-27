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
            //context.HttpContext.User.Identity != null && context.HttpContext.User.Identity.IsAuthenticated
            if (context.HttpContext.Request.Path.Value != null && !context.HttpContext.Request.Path.Value.EndsWith("/login/login", StringComparison.InvariantCultureIgnoreCase) && string.IsNullOrWhiteSpace(context.HttpContext.Session.GetString("SV_USR_EMAIL")))
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
