using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Mep01Web.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;
using MepWeb.Service.Interface;
using MepWeb.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;
using MepWeb.Type;
using MepWeb.DTO.Response;

namespace Mep01Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILoginService _loginService;
        private readonly UserScope _userScope;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor, ILoginService loginService, UserScope userScope)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
            _loginService = loginService;
            _userScope = userScope;
        }

        public IActionResult Index()
        {
			return View();
        }

        public IActionResult ISL()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
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
            var loginRequest = new { Email = email, Password = password };

            var handlerHttp = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var client = new HttpClient(handlerHttp);

            var values = new Dictionary<string, string>
                          {
                              { "Email", email },
                              { "Password", password }
                          };

            string platformUrl = "https://localhost:7182/";
            client.BaseAddress = new Uri(platformUrl);
            var conv = JsonConvert.SerializeObject(loginRequest);
            var strcontent = new StringContent(conv, System.Text.Encoding.UTF8, "application/json");
            var content = new FormUrlEncodedContent(values);
            var resp = await client.PostAsync("api/VerifyCredentials", content);
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

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}