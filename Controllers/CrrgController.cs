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
using MepWeb.Costants;
using Mep01Web.Type.Dropdown;
using System.Globalization;
using System.Reflection;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MepWeb.Controllers;

namespace Mep01Web.Controllers
{
    [Authorize]
    [SessionTimeoutFilter]
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
            DateTime dd2 = DateTime.Now.Date;
            DateTime dd1 = dd2.Subtract(TimeSpan.FromDays(((int)dd2.DayOfWeek + 6) % 7 ));
            
            CrrgGetRequest obj = new CrrgGetRequest();
            obj.FilterCrrgCRis = _userScope.SV_USR_SIGLA;
            obj.FilterCrrgDttStart = dd1;
            obj.FilterCrrgDttEnd = dd2;
            obj.FilterHidden = "Y";
            CrrgGroupByList crrgGroupByList = new CrrgGroupByList();
            obj.FilterGroupList = crrgGroupByList;
            obj.FilterGroup = "D";            
            IEnumerable<FlussoCrrg> objCrrgList = _db.FlussoCrrgs
                .Where(c => c.CrrgCRis == obj.FilterCrrgCRis && c.CrrgDtt >= obj.FilterCrrgDttStart && c.CrrgDtt <= obj.FilterCrrgDttEnd)                
                .OrderByDescending(c => c.CrrgDtIns)
                .OrderByDescending(c => c.CrrgDtt)
            .ToList();

            obj.CrrgList = objCrrgList;

            //var results = _db.FlussoCrrgs
            //	.GroupBy(x => CultureInfo.CurrentCulture.DateTimeFormat.Calendar
            //                    .GetWeekOfYear((DateTime)x.CrrgDtt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
            //       .SelectMany(gx => gx, (gx, x) => new
            //       {
            //        Week = gx.Key,
            //        DateTime = x,
            //        Count = gx.Count(),
            //       });

            //         var re = results.ToList();

            return View(obj);           
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CrrgGetRequest obj)
        {
            obj.FilterHidden = "";
            IEnumerable<FlussoCrrg> objCrrgList = null;
            switch (obj.FilterGroup)
            {
                case "D": 
                   objCrrgList = _db.FlussoCrrgs
                    .Where(c => c.CrrgCRis == obj.FilterCrrgCRis && c.CrrgDtt >= obj.FilterCrrgDttStart && c.CrrgDtt <= obj.FilterCrrgDttEnd)
                    .Where(c => obj.FilterRifCliente == "" || obj.FilterRifCliente == null || c.CrrgRifCliente.Contains(obj.FilterRifCliente)).AsEnumerable()
                    .Where(c => obj.FilterCommCode == "" || obj.FilterCommCode == null || ( c.CrrgTstDoc + "/" + c.CrrgPrfDoc + "/" + c.CrrgADoc.ToString() + "/" + c.CrrgNDoc.ToString("000000")).Contains(obj.FilterCommCode) )  
                    .OrderByDescending(c => c.CrrgDtIns)
                    .OrderByDescending(c => c.CrrgDtt)
                    .ToList();
                    break;
                case "I":
                   objCrrgList = _db.FlussoCrrgs
                    .Where(c => c.CrrgCRis == obj.FilterCrrgCRis && c.CrrgDtt >= obj.FilterCrrgDttStart && c.CrrgDtt <= obj.FilterCrrgDttEnd)
                    .Where(c => obj.FilterRifCliente == "" || obj.FilterRifCliente == null || c.CrrgRifCliente.Contains(obj.FilterRifCliente)).AsEnumerable()
                    .Where(c => obj.FilterCommCode == "" || obj.FilterCommCode == null || (c.CrrgTstDoc + "/" + c.CrrgPrfDoc + "/" + c.CrrgADoc.ToString() + "/" + c.CrrgNDoc.ToString("000000")).Contains(obj.FilterCommCode))
                    .OrderByDescending(c => c.CrrgDtIns)
                    .OrderByDescending(c => c.CrrgDtt)
                    .OrderByDescending(c => c.CrrgRifCliente)
                    .ToList();
                    break;
                case "C":
                    objCrrgList = _db.FlussoCrrgs
                     .Where(c => c.CrrgCRis == obj.FilterCrrgCRis && c.CrrgDtt >= obj.FilterCrrgDttStart && c.CrrgDtt <= obj.FilterCrrgDttEnd)
                     .Where(c => obj.FilterRifCliente == "" || obj.FilterRifCliente == null || c.CrrgRifCliente.Contains(obj.FilterRifCliente)).AsEnumerable()
                     .Where(c => obj.FilterCommCode == "" || obj.FilterCommCode == null || (c.CrrgTstDoc + "/" + c.CrrgPrfDoc + "/" + c.CrrgADoc.ToString() + "/" + c.CrrgNDoc.ToString("000000")).Contains(obj.FilterCommCode))
                     .OrderByDescending(c => c.CrrgDtIns)
                     .OrderByDescending(c => c.CrrgDtt)
                     .OrderByDescending(c => c.CrrgNDoc)
                     .OrderByDescending(c => c.CrrgADoc)
                     //.OrderByDescending(c => c.CrrgPrfDoc)
                     //.OrderByDescending(c => c.CrrgTstDoc)
                     .ToList();
                    break;
            }

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
            obj.CrrgCRis = obj.CrrgCRis.ToUpper();
			ModelState.Clear();
			var addCrrgResponse = await _crrgService.AddCrrgAsync(obj);

            if (!addCrrgResponse.Succeeded)
            {
				if (addCrrgResponse.Errors[0].Code == CrrgCreateErrors.CrrgCRis)
				{
					ModelState.AddModelError("CrrgCRis", addCrrgResponse.Errors[0].Message);
				}
				if (addCrrgResponse.Errors[0].Code == CrrgCreateErrors.CrrgTmRunIncrHMS)
                {
                    ModelState.AddModelError("CrrgTmRunIncrHMS", addCrrgResponse.Errors[0].Message);
                }
                if (addCrrgResponse.Errors[0].Code == CrrgCreateErrors.CrrgRifCliente)
                {
                    ModelState.AddModelError("CrrgRifCliente", addCrrgResponse.Errors[0].Message);
                }
				if (addCrrgResponse.Errors[0].Code == CrrgCreateErrors.CommCode)
				{
					ModelState.AddModelError("CommCode", addCrrgResponse.Errors[0].Message);
				}
				if (addCrrgResponse.Errors[0].Code == CrrgCreateErrors.NTOper)
				{
					ModelState.AddModelError("NTOper", addCrrgResponse.Errors[0].Message);
				}
				if (addCrrgResponse.Errors[0].Code == CrrgCreateErrors.CrrgCCaus)
				{
					ModelState.AddModelError("CrrgCCaus", addCrrgResponse.Errors[0].Message);
				}
                if (addCrrgResponse.Errors[0].Code == CrrgCreateErrors.CrrgCmaatt)
                {
                    ModelState.AddModelError("CrrgCmaatt", addCrrgResponse.Errors[0].Message); 
                }
				if (addCrrgResponse.Errors[0].Code == CrrgCreateErrors.CrrgNote)
				{
					ModelState.AddModelError("CrrgNote", addCrrgResponse.Errors[0].Message);
				}
				if (addCrrgResponse.Errors[0].Code == CrrgCreateErrors.CrrgApp)
				{
					ModelState.AddModelError("CrrgApp", addCrrgResponse.Errors[0].Message);
				}
				if (addCrrgResponse.Errors[0].Code == CrrgCreateErrors.CrrgMod)
				{
					ModelState.AddModelError("CrrgMod", addCrrgResponse.Errors[0].Message);
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


        //GET Create con parametro csrl        
        [HttpGet("[controller]/[action]/{crrgCSrl}")]
        public async Task<IActionResult> Duplicate(int? crrgCSrl)
        {
            if (crrgCSrl == null || crrgCSrl == 0)
            {
                return NotFound();
            }
            CrrgCreateRequest obj = new CrrgCreateRequest((decimal)crrgCSrl);
            await _crrgService.AddCrrgPrepareDataAsync(obj);
            await _crrgService.DeleteCrrgPrepareDataAsync(obj);
            if (obj.CrrgRifCliente == null) {
                obj.MemoModalita = "modCom";
			} else
            {
				obj.MemoModalita = "modIsl";
			}

            return View("Create", obj);			
        }

        //POST
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Duplicate(CrrgCreateRequest obj)
        {
            await Create(obj);
            return View("Create",obj);
        }

        [HttpGet("[controller]/[action]/{crrgCSrl}")]
        public async Task<IActionResult> Update(int crrgCSrl)
        {
            if (crrgCSrl == null || crrgCSrl == 0)
            {
                return NotFound();
            }
            CrrgCreateRequest obj = new CrrgCreateRequest((decimal)crrgCSrl);
            obj.IsUpdate = true;
            await _crrgService.AddCrrgPrepareDataAsync(obj);
            await _crrgService.DeleteCrrgPrepareDataAsync(obj);
            if (obj.CrrgRifCliente == null)
            {
                obj.MemoModalita = "modCom";
            }
            else
            {
                obj.MemoModalita = "modIsl";
            }

            return View("Create", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(CrrgCreateRequest obj)
        {
            await Create(obj);
            return View("Create", obj);
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

