using MepWeb.DTO.Request;
using MepWeb.DTO.Response;
using MepWeb.Service;
using MepWeb.Service.Impl;
using MepWeb.Service.Interface;
using MepWeb.Type;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MepWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserScope _userScope;
        private readonly ILoginService _loginService;

        public LoginController(UserScope userScope, ILoginService loginService)
        {
            _userScope = userScope;
            _loginService = loginService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task LogoutGet()
        {
            await HttpContext.SignOutAsync();
            Response.Redirect("/");
        }

        [HttpPost]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync();
            Response.Redirect("/");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var loginRequest = new LoginRequest { Email = email, Password = password };

            var resp = await _loginService.LogInAsync(loginRequest);
            if (resp.IsSuccessStatusCode)
            {
                var token = await resp.Content.ReadAsStringAsync();
                //_contextAccessor.HttpContext.Session.SetString("SC_SC_Token", tokenScc);
                var sigla = await _loginService.GetMepUsrSigla(email);
                _userScope.SV_USR_EMAIL = email;
                _userScope.SV_USR_SIGLA = sigla;

                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var identity = new ClaimsPrincipal(new ClaimsIdentity(jwtSecurityToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme));
                var authResult = await HttpContext.AuthenticateAsync();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity);
            }
            else if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return View(new LoginResponse()
                {
                    StatusType = LoginStatus.Unauthorized
                });
            }
            else
            {
                return View(new LoginResponse()
                {
                    StatusType = LoginStatus.UnknownError,
                    ResponseError = await resp.Content.ReadAsStringAsync()
                });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
