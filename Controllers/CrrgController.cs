using Microsoft.AspNetCore.Mvc;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Mep01Web.DTO.Request;
using Mep01Web.Type;
using Mep01Web.Service.Interface;

namespace Mep01Web.Controllers
{
    public class CrrgController : Controller
    {
        private readonly SataconsultingContext _db;
        private readonly ICrrgService _crrgService;
        private readonly IHttpContextAccessor _contextAccessor;

        public CrrgController(SataconsultingContext sataconsulting, ICrrgService crrgService, IHttpContextAccessor contextAccessor)
        {
            _db = sataconsulting;
            _crrgService = crrgService;
            _contextAccessor = contextAccessor;
        }
        public IActionResult Index()
        {            
            DateTime dd2 = DateTime.Now;
            DateTime dd1 = dd2.Subtract(TimeSpan.FromDays(90));
            
            CrrgGetRequest obj = new CrrgGetRequest();
            obj.FilterCrrgCRis = _contextAccessor.HttpContext.Session.GetString("User");
            obj.FilterCrrgDttStart = dd1;
            obj.FilterCrrgDttEnd = dd2;
            obj.FilterHidden = "Y";
            IEnumerable<FlussoCrrg> objCrrgList = _db.FlussoCrrgs
                .Where(c => c.CrrgCRis == obj.FilterCrrgCRis && c.CrrgDtt >= obj.FilterCrrgDttStart && c.CrrgDtt <= obj.FilterCrrgDttEnd)
                .OrderByDescending(c=> c.CrrgDttIni);
            obj.CrrgList = objCrrgList;

            return View(obj);           
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CrrgGetRequest obj)
        {
            obj.FilterHidden = "";
            IEnumerable<FlussoCrrg> objCrrgList = _db.FlussoCrrgs
                .Where(c => c.CrrgCRis == obj.FilterCrrgCRis && c.CrrgDtt >= obj.FilterCrrgDttStart && c.CrrgDtt <= obj.FilterCrrgDttEnd)
                .Where(c => obj.FilterRifCliente == "" || obj.FilterRifCliente == null || c.CrrgRifCliente.Contains(obj.FilterRifCliente))
                .OrderByDescending(c => c.CrrgDttIni);
            obj.CrrgList = objCrrgList;
            return View(obj);
        }

        //GET
        public async Task<IActionResult> Create()
        {
			CrrgCreateRequest obj = new CrrgCreateRequest();
			await _crrgService.AddCrrgPrepareDataAsync(obj);
            obj.CrrgDtt = DateTime.Today;
            
            var userId = _contextAccessor.HttpContext.Session.GetString("User");
            if (userId != null)
            {
                obj.CrrgCRis = userId;
            }

            return View(obj);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CrrgCreateRequest obj)
        //public ActionResult Create(FlussoCrrg obj)
        {
			//if (ModelState.IsValid)
			//{ 
			ModelState.Clear();
			var addCrrgResponse = await _crrgService.AddCrrgAsync(obj);

            if (!addCrrgResponse.Succeeded)
            {
				if (addCrrgResponse.Errors[0].Code == "-3")
                {
                    ModelState.AddModelError("CrrgTmRunIncrHMS", addCrrgResponse.Errors[0].Message);
                }
                if (addCrrgResponse.Errors[0].Code == "-4")
                {
                    ModelState.AddModelError("CrrgRifCliente", addCrrgResponse.Errors[0].Message);
                }
				if (addCrrgResponse.Errors[0].Code == "-5")
				{
					ModelState.AddModelError("CrrgRifCliente", addCrrgResponse.Errors[0].Message);
				}
				if (addCrrgResponse.Errors[0].Code == "-6")
				{
					ModelState.AddModelError("CommCodeDesc", addCrrgResponse.Errors[0].Message);
				}
				if (addCrrgResponse.Errors[0].Code == "-7")
				{
					ModelState.AddModelError("CommCode", addCrrgResponse.Errors[0].Message);
				}
				if (addCrrgResponse.Errors[0].Code == "-8")
				{
					ModelState.AddModelError("NTOper", addCrrgResponse.Errors[0].Message);
				}
				await _crrgService.AddCrrgPrepareDataAsync(obj);
				return View(obj);
            }
            //}
            return RedirectToAction("Create");
			//return RedirectToAction("Index");
		}



        //GET Update
        public async Task<IActionResult> Edit(int? crrgCSrl)
        {
            if (crrgCSrl == null || crrgCSrl == 0)
            {
                return NotFound();
            }
			//TODO
			CrrgCreateRequest obj = new CrrgCreateRequest();
			return View(obj);
        }

		//POST Update
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(CrrgCreateRequest obj)
        {
			//TODO
			return RedirectToAction("Index");
		}



        //GET Delete
        [HttpGet("[controller]/[action]/{crrgCSrl}")]        
        public async Task<IActionResult> Delete(int crrgCSrl)
        {
            if (crrgCSrl == null)
            {
                return NotFound();
            }            
            CrrgCreateRequest obj = new CrrgCreateRequest((decimal)crrgCSrl);
			await _crrgService.DeleteCrrgPrepareDataAsync(obj);
			return View(obj);
        }

        //POST Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(CrrgCreateRequest obj)
        {
			var deleteCrrgResponse = await _crrgService.DeleteCrrgAsync(obj);
			
			return RedirectToAction("Index");		
			
        }

    }
}

