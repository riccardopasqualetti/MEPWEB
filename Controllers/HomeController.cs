using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Mep01Web.Models;

namespace Mep01Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IHttpContextAccessor _contextAccessor;
		public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
			_contextAccessor = contextAccessor;
		}

        public IActionResult Index()
        {
            var user = _contextAccessor.HttpContext.Session.GetString("User");
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            //_contextAccessor.HttpContext.Session.SetString("User", "LD");

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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string user)
        {
            _contextAccessor.HttpContext.Session.SetString("User", user);
            return RedirectToAction("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}