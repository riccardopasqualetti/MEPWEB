using Mep01Web.DTO.Request;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Mep01Web.Models.Views;
using Mep01Web.Service.Interface;
using MepWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mep01Web.Controllers
{
    [Authorize]
    public class IslController : Controller
    {

        private readonly SataconsultingContext _db;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserScope _userScope;
		public IslController(SataconsultingContext sataconsulting, IHttpContextAccessor contextAccessor, UserScope userScope)
		{
			_db = sataconsulting;
			_contextAccessor = contextAccessor;
			_userScope = userScope;
		}

		public IActionResult Index()
        {
            IslGetRequest obj = new IslGetRequest();
            obj.FilterIslCRis = _userScope.SV_USR_SIGLA;
            obj.FilterHidden = "Y";
            IEnumerable<VsPpMonitorIsl> objIslList = _db.VsPpMonitorIsl
                .Where(c => c.CurrCdl.Contains("_" + obj.FilterIslCRis) && c.Flag != "9-CLOSE")
                .OrderBy(c => c.Flag).ThenBy(c => c.CurrDt).ThenBy(c => c.CurrCdl);
            obj.IslList = objIslList;

            return View(obj);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(IslGetRequest obj)
        {
            obj.FilterHidden = "";
            IEnumerable<VsPpMonitorIsl> objIslList = _db.VsPpMonitorIsl
                .Where(c => c.CurrCdl.Contains("_" + obj.FilterIslCRis) && c.Flag != "9-CLOSE")
                .OrderBy(c => c.Flag).ThenBy(c => c.CurrDt).ThenBy(c => c.CurrCdl);
            obj.IslList = objIslList;
            return View(obj);
        }
    }
}
