using Microsoft.EntityFrameworkCore;
using Mep01Web.Exeptions;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Mep01Web.Type;
using Mep01Web.Validators;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.DTO;
using Microsoft.AspNetCore.Mvc;
using Mep01Web.Validators.Impl;
using Mep01Web.Service.Interface;
using Mep01Web.Service.Impl;
using Mep01Web.Type.Dropdown;

namespace Mep01Web.Service.Impl
{
    public class CrrgService : ICrrgService
    {
        private readonly SataconsultingContext _dbContext;

        private readonly ICrrgValidator _crrgValidator;
        private readonly IAcliService _acliService;
        private readonly ITbcpService _tbcpService;
        private readonly IOlcaService _olcaService;
		private readonly IMvxpa01Service _mvxpa01Service;		
		private readonly ITbpnService _tbpnService;
		private readonly ITatvService _tatvService;

		public CrrgService(SataconsultingContext dbContext, ICrrgValidator crrgValidator, IAcliService acliService, ITbcpService tbcpService, IOlcaService olcaService, IMvxpa01Service mvxpa01Service, ITbpnService tbpnService, ITatvService tatvService)
        {
            _dbContext = dbContext;
            _crrgValidator = crrgValidator;
            _acliService = acliService;
            _tbcpService = tbcpService;
			_olcaService = olcaService;
			_mvxpa01Service = mvxpa01Service;
            _tbpnService = tbpnService;
            _tatvService = tatvService;
		}

        public async Task<ResponseBase<CrrgResponse>?> GetCrrgAsync(CrrgCreateRequest crrgRequest)
        {
            FlussoCrrg flussoCrrg = await _dbContext.FlussoCrrgs.SingleOrDefaultAsync(e => e.CrrgCSrl == crrgRequest.CrrgCSrl && e.CrrgCDitta == "01");

            if (flussoCrrg == null)
            {
                return ResponseBase<CrrgResponse?>.Failed("-1", $"Non sono stati trovati record con questo valore '{crrgRequest.CrrgCSrl}'");
            }
            var crrgResponse = new CrrgResponse();
            crrgResponse.FlussoCrrg = flussoCrrg;
            return ResponseBase<CrrgResponse?>.Success(crrgResponse);
        }


        public async Task<ResponseBase<CrrgResponse>?> AddCrrgAsync(CrrgCreateRequest crrgRequest)
        {
			
			var crrgValidate = await _crrgValidator.CrrgValidateAsync(crrgRequest);
            if (!crrgValidate.Succeeded)
            {
                return ResponseBase<CrrgResponse?>.Failed(crrgValidate.Errors);
            }

            var m = _dbContext.FlussoCrrgs.Max(c => c.CrrgCSrl) + 1;
            var crrgTmRunIncr = new Duration(crrgRequest.CrrgTmRunIncrHMS);
            var hms = crrgTmRunIncr.GetSeconds();
            //var hms = crrgRequest.CrrgTmRunIncrHMS.Hour * 3600 + crrgRequest.CrrgTmRunIncrHMS.Minute * 60 + crrgRequest.CrrgTmRunIncrHMS.Second;
            DateTime dt = DateTime.Now;
            var flussoCrrg = new FlussoCrrg
            {
                CrrgCSrl = m,
                CrrgTstDoc = crrgRequest.CrrgTstDoc,
                CrrgPrfDoc = crrgRequest.CrrgPrfDoc,
                CrrgADoc = crrgRequest.CrrgADoc,
                CrrgNDoc = crrgRequest.CrrgNDoc,
                CrrgPosDoc = crrgRequest.CrrgPosDoc,
                CrrgPrgDoc = crrgRequest.CrrgPrgDoc,
                CrrgNOper = crrgRequest.CrrgNOper,
                CrrgTRis = "3",
                CrrgCRis = crrgRequest.CrrgCRis,
                CrrgDtt = crrgRequest.CrrgDtt,
                CrrgCDitta = "01",
                CrrgTOperFdc = "S",
                CrrgFlgDomina = "S",
                CrrgCCaus = crrgRequest.CrrgCCaus,
                CrrgCSeq = "18",
                CrrgNOperOlca = crrgRequest.CrrgNOper,
                CrrgTOper = crrgRequest.CrrgTOper,
                CrrgCdl = crrgRequest.CrrgCdl,
                //CrrgTmAttrIncr = 0
                CrrgTmRunIncr = hms,
                //CrrgQtaVrst = 0
                //CrrgCLtt = NULL
                //CrrgQtaScarto = 0
                //CrrgPercComp = 0
                //CrrgQtaPrel = 0
                CrrgDttApertura = dt.Date,
                CrrgDttIni = dt.Date,
                CrrgDttFine = dt.Date,
                //CrrgDttSosp = NULL
                //CrrgDttRipr = NULL
                //CrrgDttChiusura = NULL
                CrrgNTerm = "$SERVER$",
                //CrrgNRif = 0
                //CrrgErrExt = 0
                //CrrgErrExtDt = 0
                //CrrgErrOper = 0
                //CrrgErrQta = 0
                //CrrgErrRis = 0
                //CrrgErrRisDt = 0
                //CrrgErrDt = 0
                //CrrgErrPres = 0
                //CrrgErr1 = 0
                //CrrgErr2 = 0
                //CrrgFlgErrClear = NULL
                //CrrgFlgCheck = NULL
                CrrgCSrlPadre = m,
                CrrgCSrlFiglio = m,
                CrrgFlgElab = "S",
                Crrg01Ac = 4,
                CrrgAComp = dt.Year,
                CrrgMeseComp = dt.Month,
                CrrgDtIns = dt,
                CrrgUtenteIns = crrgRequest.CrrgCRis,
                CrrgDtUm = dt,
                CrrgUtenteUm = crrgRequest.CrrgCRis,
                CrrgCmaatt = crrgRequest.CrrgCmaatt,
                CrrgNote = crrgRequest.CrrgNote,
                //CrrgN0dIdxolcm = NULL
                //CrrgCcausfdc = 0
                //CrrgCAddetto = NULL
                //CrrgCFormato = NULL
                //CrrgCPart = NULL
                //CrrgCInsieme = NULL
                //CrrgQtaXInsieme = 0
                //CrrgFlgElabPresidio = NULL
                //CrrgTurno = 0
                //CrrgTurnoData = NULL
                //CrrgFlgEsito = NULL
                //CrrgFlgQtaRilavora = NULL
                CrrgRifCliente = crrgRequest.CrrgRifCliente,
                CrrgTmRunIncrProd = hms,
                //CrrgM1ObjtypeOdl = 0
                //CrrgM1DocentryOdl = 0
                //CrrgNumVerbale = NULL
                CrrgApp = crrgRequest.CrrgApp,
                CrrgMod = crrgRequest.CrrgMod
			};
            _dbContext.FlussoCrrgs.Add(flussoCrrg);
            var affected = await _dbContext.SaveChangesAsync();

            var crrgResponse = new CrrgResponse();
            crrgResponse.FlussoCrrg = flussoCrrg;
            return ResponseBase<CrrgResponse?>.Success(crrgResponse);
        }


        public async Task<ResponseBase<CrrgResponse>?> DeleteCrrgAsync(CrrgCreateRequest crrgRequest)
        {
			FlussoCrrg flussoCrrg = await _dbContext.FlussoCrrgs.SingleOrDefaultAsync(e => e.CrrgCSrl == crrgRequest.CrrgCSrl && e.CrrgCDitta == "01");
            if (flussoCrrg == null)
            {
				return ResponseBase<CrrgResponse?>.Failed("-2", $"Non sono stati trovati record in flusso_acli");
			}
            _dbContext.FlussoCrrgs.Remove(flussoCrrg);
			var affected = await _dbContext.SaveChangesAsync();

			var crrgResponse = new CrrgResponse();
			crrgResponse.FlussoCrrg = flussoCrrg;
			return ResponseBase<CrrgResponse?>.Success(crrgResponse);
		}


		public async Task<CrrgCreateRequest> AddCrrgPrepareDataAsync(CrrgCreateRequest crrgCreateRequest)
        {

            // Se ISL risulta valorizzata, imposto la commessa in CommCode e CommCodeDesc che essendo protette vengono ripulite
            //if (!string.IsNullOrWhiteSpace(crrgCreateRequest.CrrgRifCliente))
            //{
            //    TatvGetRequest tatvGetRequest = new TatvGetRequest();
            //    tatvGetRequest.codeISl = crrgCreateRequest.CrrgRifCliente;
            //    var tatv = await _tatvService.GetTatvAsync(tatvGetRequest);
            //    if (tatv != null)
            //    {
            //        crrgCreateRequest.CommCode = tatv.Body.ISLMasterData.TatvTstComm + "/" + tatv.Body.ISLMasterData.TatvPrfComm + "/" + tatv.Body.ISLMasterData.TatvAComm + "/" + tatv.Body.ISLMasterData.TatvNComm;
            //        crrgCreateRequest.CommCodeDesc = tatv.Body.ISLCommData.CommMasterData.TbcpDesc;
            //    }
            //}


            // Preparazione dropdown con causali
            CrrgCCausList crrgCCausList = new CrrgCCausList();
			crrgCreateRequest.CrrgCCausList = crrgCCausList;
            		

			// Preparazione dropdown con macroattività
			CrrgCmaattList crrgCmaattList = new CrrgCmaattList();
			var getMvxpa01response = await _mvxpa01Service.GetMvxpa01Async();
			foreach (var mvxpa01 in getMvxpa01response.Body)
			{
				var item = new SelectListItem
				{
					Text = mvxpa01.Descrizione,
					Value = mvxpa01.Cmaatt
				};

				crrgCmaattList.Lista.Add(item);
			}
			crrgCreateRequest.CrrgCmaattList = crrgCmaattList;
			

            // Preparazione dropdown con codice e ragione sociale clienti
			AcliList acliList = new AcliList();
            var getAcliAllResponse = await _acliService.GetAcliAllAsync();
            foreach (var acli in getAcliAllResponse.Body)
            {
                var item = new SelectListItem
                {
                    Text = acli.FlussoAcli.AcliRagSoc1 + " - " + acli.FlussoAcli.AcliCCliente,
                    Value = acli.FlussoAcli.AcliCCliente
                };

                acliList.Clienti.Add(item);
            }
            crrgCreateRequest.AcliList = acliList;


            // Preparazione dropdown con codice e descrizione commesse            
			crrgCreateRequest.TbcpList = new TbcpList();
			

			// Preparazione dropdown con numero operazioni e tipo operazioni
			NTOperList crrgTOperList = new NTOperList();					
			//OlcaGetRequest o = new OlcaGetRequest();
			//o.OlcaTstDoc = "COV";
			//o.OlcaPrfDoc = "A";
			//o.OlcaADoc = 2023;
			//o.OlcaNDoc = 231006;
			//o.OlcaNRigaDoc = 1;
			//var getOlcaCitoResponse = await _olcaService.GetOlcaCitoAsync(o);
            crrgCreateRequest.NTOperList = new NTOperList();


			// Preparazione dropdown Applicativi
			AppList appList = new AppList();
			TbpnGetRequest tbpnAppGetRequest= new TbpnGetRequest();
            tbpnAppGetRequest.TbpnCPart = "APP_";
			tbpnAppGetRequest.TbpnCDitta = "01";
			var getTbpnAppAllLightAsync = await _tbpnService.GetTbpnLikeAllLightAsync(tbpnAppGetRequest);
			foreach (var tbpnLight in getTbpnAppAllLightAsync.Body)
			{
				var item = new SelectListItem
				{
					Text = tbpnLight.TbpnCPart,
					Value = tbpnLight.TbpnCPart
				};

				appList.Applicativi.Add(item);
			}			
			crrgCreateRequest.AppList = appList;


			// Preparazione dropdown Moduli
			ModList modList = new ModList();
			TbpnGetRequest tbpnModGetRequest = new TbpnGetRequest();
			tbpnModGetRequest.TbpnCPart = "MOD_";
			tbpnModGetRequest.TbpnCDitta = "01";
			var getTbpnModAllLightAsync = await _tbpnService.GetTbpnLikeAllLightAsync(tbpnModGetRequest);
			foreach (var tbpnLight in getTbpnModAllLightAsync.Body)
			{
				var item = new SelectListItem
				{
					Text = tbpnLight.TbpnCPart,
					Value = tbpnLight.TbpnCPart
				};

				modList.Moduli.Add(item);
			}
			crrgCreateRequest.ModList = modList;


			return crrgCreateRequest;
        }

        public async Task<CrrgCreateRequest> DeleteCrrgPrepareDataAsync(CrrgCreateRequest crrgCreateRequest)
        {
            var getCrrgResponse = await GetCrrgAsync(crrgCreateRequest);
            var flussoCrrg = getCrrgResponse.Body.FlussoCrrg;
			crrgCreateRequest.CrrgCSrl = flussoCrrg.CrrgCSrl;
			crrgCreateRequest.CrrgCRis = flussoCrrg.CrrgCRis;
			crrgCreateRequest.CrrgDtt = flussoCrrg.CrrgDtt == null ? DateTime.Now : (DateTime)flussoCrrg.CrrgDtt;            
			crrgCreateRequest.CrrgTmRunIncrHMS = (new Duration(flussoCrrg.CrrgTmRunIncr)).GetDatetime();
			crrgCreateRequest.CrrgRifCliente = flussoCrrg.CrrgRifCliente;
			crrgCreateRequest.CommCode = flussoCrrg.CrrgTstDoc + "/" + flussoCrrg.CrrgPrfDoc + "/" + flussoCrrg.CrrgADoc + "/" + flussoCrrg.CrrgNDoc.ToString("000000"); ;
            crrgCreateRequest.CrrgNote = flussoCrrg.CrrgNote;


            var tbcpGetRequest = new TbcpGetRequest();
            tbcpGetRequest.CommPrfDoc = flussoCrrg.CrrgPrfDoc;
			tbcpGetRequest.CommTstDoc = flussoCrrg.CrrgTstDoc;
			tbcpGetRequest.CommADoc = flussoCrrg.CrrgADoc;
			tbcpGetRequest.CommNDoc = flussoCrrg.CrrgNDoc;
            var getTbcpResponse = await _tbcpService.GetTbcpByCodeAsync(tbcpGetRequest);
            if (getTbcpResponse.Succeeded)
            {
				crrgCreateRequest.ComCCli = getTbcpResponse.Body.CommMasterData.TbcpCCli + " - " + getTbcpResponse.Body.CommRCli;
                crrgCreateRequest.CommCodeDesc = crrgCreateRequest.CommCode + " - " + getTbcpResponse.Body.CommMasterData.TbcpDesc;
			}			
			return crrgCreateRequest;
		}
    }
}

