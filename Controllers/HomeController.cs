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
using MepWeb.DTO.Request;
using MepWeb.Controllers;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}