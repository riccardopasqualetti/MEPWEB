using Microsoft.AspNetCore.Mvc;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Mep01Web.DTO.Request;
using Mep01Web.Type;
using Mep01Web.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using MepWeb.Service;
using MepWeb.Service.Interface;
using MepWeb.DTO.Request;

namespace Mep01Web.Controllers
{
    [Authorize]
    public class CrrgController : Controller
    {
        private readonly SataconsultingContext _db;
        private readonly ICrrgService _crrgService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserScope _userScope;
        private readonly IRegistroRicaricheService _registroRicaricheService;
		private readonly IOreQualificaService _oreQualificaService;

		public CrrgController(SataconsultingContext sataconsulting, ICrrgService crrgService, IHttpContextAccessor contextAccessor, UserScope userScope, IRegistroRicaricheService registroRicaricheService, IOreQualificaService oreQualificaService)
		{
			_db = sataconsulting;
			_crrgService = crrgService;
			_contextAccessor = contextAccessor;
			_userScope = userScope;
			_registroRicaricheService = registroRicaricheService;
			_oreQualificaService = oreQualificaService;
		}
		public IActionResult Index()
        {            
            DateTime dd2 = DateTime.Now;
            DateTime dd1 = dd2.Subtract(TimeSpan.FromDays(90));
            
            CrrgGetRequest obj = new CrrgGetRequest();
            obj.FilterCrrgCRis = _userScope.SV_USR_SIGLA;
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
            
            var userId = _userScope.SV_USR_SIGLA;
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
				if (addCrrgResponse.Errors[0].Code == "-11")
				{
					ModelState.AddModelError("CrrgCCaus", addCrrgResponse.Errors[0].Message);
				}
                if (addCrrgResponse.Errors[0].Code == "-12")
                {
                    ModelState.AddModelError("CrrgCmaatt", addCrrgResponse.Errors[0].Message); 
                }

                obj.Succeeded = "N";
				//await _crrgService.AddCrrgPrepareDataAsync(obj);
				//return View(obj);
            }
            else
            {
				obj.Succeeded = "S";
			}
			//}
			await _crrgService.AddCrrgPrepareDataAsync(obj);
			return View(obj);
			//return RedirectToAction("Create");
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

        [HttpGet]
        public async Task<IActionResult> IndexConsuntivi()
        {
            var res = await _crrgService.GetAllConsAsync();
            var bod = res.Body;
            return View(res.Body);
        }

        [HttpGet("Crrg/OreQualifica/{idDoc}")]
        public async Task<IActionResult> OreQualifica(decimal idDoc)
		{
			var request = new IdDocumentoRequest()
			{
				idDocumento = idDoc
			};
			return View(request);
		}

        [HttpGet("Crrg/AddettoQualifica/{idDoc}")]
        public async Task<IActionResult> AddettoQualifica(decimal idDoc)
		{
			var request = new IdDocumentoRequest()
			{
				idDocumento = idDoc
			};
			return View(request);
		}

        [HttpGet("Crrg/RegistroRicariche/{idDoc}/{qualifica}")]
        public async Task<IActionResult> RegistroRicariche(decimal idDoc, decimal qualifica)
		{
			var request = new IdDocumentoRequest()
			{
				idDocumento = idDoc
			};
			return View(request);
		}

    }
}

