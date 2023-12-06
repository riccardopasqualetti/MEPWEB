using Mep01Web.DTO.Request;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Mep01Web.Models.Views;
using Mep01Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Mep01Web.Controllers
{
    public class IslController : Controller
    {

        private readonly SataconsultingContext _db;
        private readonly IHttpContextAccessor _contextAccessor;

        public IslController(SataconsultingContext sataconsulting, IHttpContextAccessor contextAccessor)
        {
            _db = sataconsulting;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            IslGetRequest obj = new IslGetRequest();
            obj.FilterIslCRis = _contextAccessor.HttpContext.Session.GetString("User");
            obj.FilterHidden = "Y";
            IEnumerable<VsPpSprintConsComIsl> objIslList = _db.VsPpSprintConsComIsls
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
            IEnumerable<VsPpSprintConsComIsl> objIslList = _db.VsPpSprintConsComIsls
                .Where(c => c.CurrCdl.Contains("_" + obj.FilterIslCRis) && c.Flag != "9-CLOSE")
                .OrderBy(c => c.Flag).ThenBy(c => c.CurrDt).ThenBy(c => c.CurrCdl);
            obj.IslList = objIslList;
            return View(obj);
        }
    }
}
