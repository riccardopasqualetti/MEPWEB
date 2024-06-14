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
using MepWeb.DTO.Response;
using MepWeb.DTO.Request;
using MepWeb.Costants;
using MepWeb.Service.Interface;
using Mep01Web.Models.Views;
using System.Collections.Generic;
using System.Linq;

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
        private readonly IVsCommAperteXCliService _vsCommAperteXCliService;

        public CrrgService(SataconsultingContext dbContext, ICrrgValidator crrgValidator, IAcliService acliService, ITbcpService tbcpService, IOlcaService olcaService, IMvxpa01Service mvxpa01Service, ITbpnService tbpnService, ITatvService tatvService, IVsCommAperteXCliService vsCommAperteXCliService)
        {
            _dbContext = dbContext;
            _crrgValidator = crrgValidator;
            _acliService = acliService;
            _tbcpService = tbcpService;
            _olcaService = olcaService;
            _mvxpa01Service = mvxpa01Service;
            _tbpnService = tbpnService;
            _tatvService = tatvService;
            _vsCommAperteXCliService = vsCommAperteXCliService;
        }

        public async Task<ResponseBase<CrrgResponse>?> GetCrrgAsync(CrrgGetRequest crrgRequest)
        {
            FlussoCrrg flussoCrrg = await _dbContext.FlussoCrrgs.SingleOrDefaultAsync(e => e.CrrgCSrl == crrgRequest.CrrgCSrl && e.CrrgCDitta == crrgRequest.CrrgCDitta);

            if (flussoCrrg == null)
            {
                return ResponseBase<CrrgResponse?>.Failed("-1", $"Non sono stati trovati record con questo valore '{crrgRequest.CrrgCSrl}'");
            }
            var crrgResponse = new CrrgResponse();
            crrgResponse.FlussoCrrg = flussoCrrg;
            return ResponseBase<CrrgResponse?>.Success(crrgResponse);
        }

        public async Task<ResponseBase<IEnumerable<FlussoCrrg>>?> GetCrrgReportAsync(CrrgReportRequest obj)
        {
            List<FlussoCrrg> objCrrgList = null;
            //IEnumerable<FlussoCrrg> objCrrgList = null;            
            switch (obj.FilterGroup)
            {
                case "D":
                    objCrrgList = _dbContext.FlussoCrrgs
                    .Where(c => c.CrrgCRis == obj.FilterCrrgCRis && c.CrrgDtt >= obj.FilterCrrgDttStart && c.CrrgDtt <= obj.FilterCrrgDttEnd)
                    .Where(c => obj.FilterRifCliente == "" || obj.FilterRifCliente == null || c.CrrgRifCliente.Contains(obj.FilterRifCliente)).AsEnumerable()
                    .Where(c => obj.FilterCommCode == "" || obj.FilterCommCode == null || (c.CrrgTstDoc + "/" + c.CrrgPrfDoc + "/" + c.CrrgADoc.ToString() + "/" + c.CrrgNDoc.ToString("000000")).Contains(obj.FilterCommCode))
                    .OrderByDescending(c => c.CrrgDtIns)
                    .OrderByDescending(c => c.CrrgDtt)
                    .ToList();
                    break;
                case "I":
                    objCrrgList = _dbContext.FlussoCrrgs
                    .Where(c => c.CrrgCRis == obj.FilterCrrgCRis && c.CrrgDtt >= obj.FilterCrrgDttStart && c.CrrgDtt <= obj.FilterCrrgDttEnd)
                    .Where(c => obj.FilterRifCliente == "" || obj.FilterRifCliente == null || c.CrrgRifCliente.Contains(obj.FilterRifCliente)).AsEnumerable()
                    .Where(c => obj.FilterCommCode == "" || obj.FilterCommCode == null || (c.CrrgTstDoc + "/" + c.CrrgPrfDoc + "/" + c.CrrgADoc.ToString() + "/" + c.CrrgNDoc.ToString("000000")).Contains(obj.FilterCommCode))
                    .OrderByDescending(c => c.CrrgDtIns)
                    .OrderByDescending(c => c.CrrgDtt)
                    .OrderByDescending(c => c.CrrgRifCliente)
                    .ToList();
                    break;
                case "C":
                    objCrrgList = _dbContext.FlussoCrrgs
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

            //IEnumerable<FlussoCrrg> objCrrgListIE = objCrrgList.AsEnumerable();
            return ResponseBase<IEnumerable<FlussoCrrg>>.Success(objCrrgList.AsEnumerable()); //objCrrgList.AsEnumerable()
        }

        public async Task<ResponseBase<List<ConsXCommResponse>>?> GetAllConsAsync()
        {
            var res = await _dbContext.VsConsXComms.Select(item => new ConsXCommResponse
            {
                TBCP_TST_COMM = item.TbcpTstComm,
                TBCP_PRF_COMM = item.TbcpPrfComm,
                TBCP_A_COMM = item.TbcpAComm,
                TBCP_N_COMM = item.TbcpNComm,
                TBCP_C_CLI = item.TbcpCCli,
                ACLI_RAG_SOC_1 = item.AcliRagSoc1,
                TBCP_OFF_PREV = item.TbcpOffPrev,
                TBCP_RIF_CLIENTE = item.TbcpRifCliente,
                DESCRIZIONE_RIDOTTA = item.DescrizioneRidotta,
                TBCP_M1_PROJECT = item.TbcpM1Project,
                TBCP_DESC = item.TbcpDesc,
                USR1_DESC = item.Usr1Desc,
                TbcpId = item.TbcpId,
                HHACQPGM = item.HhacqPgm,
                HHACQSOA = item.HhacqSoa,
                HHACQPJM = item.HhacqPjm,
                HHACQBUC = item.HhacqBuc,
                HHACQSYD = item.HhacqSyd,
                HHACQGEN = item.HhacqGen,
                HHACQGDE = item.HhacqGde,
                TFATTPGM = item.TfattPgm,
                TFATTSOA = item.TfattSoa,
                TFATTPJM = item.TfattPjm,
                TFATTBUC = item.TfattBuc,
                TFATTSYD = item.TfattSyd,
                TFATTGEN = item.TfattGen,
                TFATTGDE = item.TfattGde,
                //HHCRRGPGM = item.HhcrrgPgm,
                //HHCRRGSOA = item.HhcrrgSoa,
                //HHCRRGPJM = item.HhcrrgPjm,
                //HHCRRGBUC = item.HhcrrgBuc,
                //HHCRRGSYD = item.HhcrrgSyd,
                //HHCRRGGEN = item.HhcrrgGen,
                HHCRRGPGMEFF = item.HhcrrgPgmEff,
                HHCRRGSOAEFF = item.HhcrrgSoaEff,
                HHCRRGPJMEFF = item.HhcrrgPjmEff,
                HHCRRGBUCEFF = item.HhcrrgBucEff,
                HHCRRGSYDEFF = item.HhcrrgSydEff,
                HHCRRGGENEFF = item.HhcrrgGenEff,
                HHCRRGGDEEFF = item.HhcrrgGdeEff,
                HHCRRGPGMEFFNV = item.HhcrrgPgmEffNv,
                HHCRRGSOAEFFNV = item.HhcrrgSoaEffNv,
                HHCRRGPJMEFFNV = item.HhcrrgPjmEffNv,
                HHCRRGBUCEFFNV = item.HhcrrgBucEffNv,
                HHCRRGSYDEFFNV = item.HhcrrgSydEffNv,
                HHCRRGGENEFFNV = item.HhcrrgGenEffNv,
                HHCRRGGDEEFFNV = item.HhcrrgGdeEffNv,
                HH001APGM = item.Hh001aPgm ?? 0,
                HH001ASOA = item.Hh001aSoa ?? 0,
                HH001APJM = item.Hh001aPjm ?? 0,
                HH001ABUC = item.Hh001aBuc ?? 0,
                HH001ASYD = item.Hh001aSyd ?? 0,
                Preoccupazione = _dbContext.FlussoTbcps.FirstOrDefault(x => x.TbcpTstComm == item.TbcpTstComm && x.TbcpPrfComm == item.TbcpPrfComm && x.TbcpAComm == item.TbcpAComm && x.TbcpNComm == item.TbcpNComm).TbcpFfllA1,
                Avanzamento = _dbContext.FlussoTbcps.FirstOrDefault(x => x.TbcpTstComm == item.TbcpTstComm && x.TbcpPrfComm == item.TbcpPrfComm && x.TbcpAComm == item.TbcpAComm && x.TbcpNComm == item.TbcpNComm).TbcpFfllN1,
                Note = _dbContext.FlussoTbcps.FirstOrDefault(x => x.TbcpTstComm == item.TbcpTstComm && x.TbcpPrfComm == item.TbcpPrfComm && x.TbcpAComm == item.TbcpAComm && x.TbcpNComm == item.TbcpNComm).TbcpFfllT1 
            }).ToListAsync();

            return res;
        }

        public async Task<ResponseBase<CrrgResponse>?> AddCrrgAsync(CrrgCreateRequest crrgRequest)
        {

            var crrgValidate = await _crrgValidator.CrrgValidateAsync(crrgRequest);
            if (!crrgValidate.Succeeded)
            {
                return ResponseBase<CrrgResponse?>.Failed(crrgValidate.Errors);
            }

            var grpCdlRequest = new CrrgGrpCdlsRequest
            {
                CrrgCdl = crrgRequest.CrrgCdl,
                CrrgCRis = crrgRequest.CrrgCRis,
                CrrgTstDoc = crrgRequest.CrrgTstDoc,
                CrrgPrfDoc = crrgRequest.CrrgPrfDoc,
                CrrgADoc = crrgRequest.CrrgADoc,
                CrrgNDoc = crrgRequest.CrrgNDoc
            };

            var grpRes = await GetGrpCdlsAsync(grpCdlRequest);

            //            tatv_residuo_gg
            //              tatv_residuo_gg_test
            //              tatv_stima_gg
            //                tatv_stima_gg_test
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
                CrrgMod = crrgRequest.CrrgMod,
                CrrgGrpcdlEff = grpRes.Body.CrrgGrpcdlEff,
                CrrgGrpcdlPrev = grpRes.Body.CrrgGrpcdlPrev

            };

            if (crrgRequest.IsUpdate)
            {
                return await UpdateCrrgAsync(crrgRequest);
            }
            else
            {
                var diff = (decimal)hms / (3600 * 8);
                if (crrgRequest.CrrgRifCliente != null && (crrgRequest.CrrgCCaus == "DELI" || crrgRequest.CrrgCCaus == "SVIL"))
                {
                    await _tatvService.UpdateTatvAsync(crrgRequest.CrrgRifCliente, crrgRequest.CrrgCCaus, diff);
                }
                _dbContext.FlussoCrrgs.Add(flussoCrrg);
            }

            var affected = await _dbContext.SaveChangesAsync();

            var crrgResponse = new CrrgResponse();
            crrgResponse.FlussoCrrg = flussoCrrg;
            return ResponseBase<CrrgResponse?>.Success(crrgResponse);
        }

        public async Task<ResponseBase<CrrgResponse>?> UpdateCrrgAsync(CrrgCreateRequest crrgRequest)
        {
            var grpCdlRequest = new CrrgGrpCdlsRequest
            {
                CrrgCdl = crrgRequest.CrrgCdl,
                CrrgCRis = crrgRequest.CrrgCRis,
                CrrgTstDoc = crrgRequest.CrrgTstDoc,
                CrrgPrfDoc = crrgRequest.CrrgPrfDoc,
                CrrgADoc = crrgRequest.CrrgADoc,
                CrrgNDoc = crrgRequest.CrrgNDoc
            };

            var grpRes = await GetGrpCdlsAsync(grpCdlRequest);

            var crrgTmRunIncr = new Duration(crrgRequest.CrrgTmRunIncrHMS);
            var hms = crrgTmRunIncr.GetSeconds();

            var record = await _dbContext.FlussoCrrgs.FirstOrDefaultAsync(x => x.CrrgCSrl == crrgRequest.CrrgCSrl);
            var hmsNew = record.CrrgTmRunIncr;
            var hmsDiff = (decimal)hms - hmsNew;


            record.CrrgTstDoc = crrgRequest.CrrgTstDoc;
            record.CrrgPrfDoc = crrgRequest.CrrgPrfDoc;
            record.CrrgADoc = crrgRequest.CrrgADoc;
            record.CrrgNDoc = crrgRequest.CrrgNDoc;
            record.CrrgPosDoc = crrgRequest.CrrgPosDoc;
            record.CrrgPrgDoc = crrgRequest.CrrgPrgDoc;
            record.CrrgNOper = crrgRequest.CrrgNOper;
            record.CrrgCRis = crrgRequest.CrrgCRis;
            record.CrrgDtt = crrgRequest.CrrgDtt;
            record.CrrgCCaus = crrgRequest.CrrgCCaus;
            record.CrrgNOperOlca = crrgRequest.CrrgNOper;
            record.CrrgTOper = crrgRequest.CrrgTOper;
            record.CrrgCdl = crrgRequest.CrrgCdl;
            record.CrrgTmRunIncr = hms;
            record.CrrgCSrlPadre = crrgRequest.CrrgCSrl;
            record.CrrgCSrlFiglio = crrgRequest.CrrgCSrl;
            record.CrrgDtUm = DateTime.Now;
            record.CrrgUtenteUm = crrgRequest.CrrgCRis;
            record.CrrgCmaatt = crrgRequest.CrrgCmaatt;
            record.CrrgNote = crrgRequest.CrrgNote;
            record.CrrgRifCliente = crrgRequest.CrrgRifCliente;
            record.CrrgTmRunIncrProd = hms;
            record.CrrgApp = crrgRequest.CrrgApp;
            record.CrrgMod = crrgRequest.CrrgMod;
            record.CrrgGrpcdlEff = grpRes.Body.CrrgGrpcdlEff;
            record.CrrgGrpcdlPrev = grpRes.Body.CrrgGrpcdlPrev;

            var affected = await _dbContext.SaveChangesAsync();

            var ggDiff = (decimal)hmsDiff / (3600 * 8);

            {
                if (crrgRequest.CrrgRifCliente != null && (crrgRequest.CrrgCCaus == "DELI" || crrgRequest.CrrgCCaus == "SVIL"))
                    await _tatvService.UpdateTatvAsync(crrgRequest.CrrgRifCliente, crrgRequest.CrrgCCaus, ggDiff);
            }

            var crrgResponse = new CrrgResponse();
            crrgResponse.FlussoCrrg = record;

            return ResponseBase<CrrgResponse?>.Success(crrgResponse);
        }


        public async Task<ResponseBase<CrrgResponse>?> DeleteCrrgAsync(CrrgCreateRequest crrgRequest)
        {
            FlussoCrrg flussoCrrg = await _dbContext.FlussoCrrgs.SingleOrDefaultAsync(e => e.CrrgCSrl == crrgRequest.CrrgCSrl && e.CrrgCDitta == "01");
            if (flussoCrrg == null)
            {
                return ResponseBase<CrrgResponse?>.Failed("-2", $"Non sono stati trovati record in flusso_acli");
            }

            var hms = new Duration(flussoCrrg.CrrgTmRunIncr).GetSeconds();
            var diff = -(decimal)hms / (3600 * 8);
            if (crrgRequest.CrrgRifCliente != null && (crrgRequest.CrrgCCaus == "DELI" || crrgRequest.CrrgCCaus == "SVIL"))
            {
                await _tatvService.UpdateTatvAsync(crrgRequest.CrrgRifCliente, crrgRequest.CrrgCCaus, diff);
            }
            _dbContext.FlussoCrrgs.Remove(flussoCrrg);
            var affected = await _dbContext.SaveChangesAsync();

            var crrgResponse = new CrrgResponse();
            crrgResponse.FlussoCrrg = flussoCrrg;
            return ResponseBase<CrrgResponse?>.Success(crrgResponse);
        }


        public async Task<CrrgCreateRequest> AddCrrgPrepareDataAsync(CrrgCreateRequest crrgCreateRequest)
        {
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
            var getAcliAllResponse = await _vsCommAperteXCliService.GetCustOfOpenCommAsync();
            foreach (var cli in getAcliAllResponse.Body)
            {
                var item = new SelectListItem
                {
                    Text = cli.CCliRag,
                    Value = cli.TbcpCCli
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
            TbpnGetRequest tbpnAppGetRequest = new TbpnGetRequest();
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
            CrrgGetRequest crrgGetRequest = new CrrgGetRequest();
            crrgGetRequest.CrrgCDitta = "01";
            crrgGetRequest.CrrgCSrl = crrgCreateRequest.CrrgCSrl;
            var getCrrgResponse = await GetCrrgAsync(crrgGetRequest);
            var flussoCrrg = getCrrgResponse.Body.FlussoCrrg;
            crrgCreateRequest.CrrgCSrl = flussoCrrg.CrrgCSrl;
            crrgCreateRequest.CrrgCRis = flussoCrrg.CrrgCRis;
            crrgCreateRequest.CrrgDtt = flussoCrrg.CrrgDtt == null ? DateTime.Now : (DateTime)flussoCrrg.CrrgDtt;
            crrgCreateRequest.CrrgTmRunIncrHMS = (new Duration(flussoCrrg.CrrgTmRunIncr)).GetDatetime();
            crrgCreateRequest.CrrgRifCliente = flussoCrrg.CrrgRifCliente;
            crrgCreateRequest.CommCode = flussoCrrg.CrrgTstDoc + "/" + flussoCrrg.CrrgPrfDoc + "/" + flussoCrrg.CrrgADoc + "/" + flussoCrrg.CrrgNDoc.ToString("000000"); ;

            crrgCreateRequest.CrrgCCaus = flussoCrrg.CrrgCCaus;
            crrgCreateRequest.CrrgNOper = flussoCrrg.CrrgNOper;
            crrgCreateRequest.CrrgTOper = flussoCrrg.CrrgTOper;
            crrgCreateRequest.CrrgCmaatt = flussoCrrg.CrrgCmaatt;
            crrgCreateRequest.CrrgApp = flussoCrrg.CrrgApp;
            crrgCreateRequest.CrrgMod = flussoCrrg.CrrgMod;
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
            }
            return crrgCreateRequest;
        }

        public async Task<decimal> CheckHoursAvailability(decimal idDoc, decimal hours, string qualifica)
        {
            var consXComm = await _dbContext.VsConsXComms.FirstOrDefaultAsync(x => x.TbcpId == idDoc);
            if (consXComm == null)
            {
                return -1;
            }
            decimal res = 0;
            switch (qualifica)
            {
                case "PGM":
                    res = consXComm.HhacqPgm - consXComm.HhcrrgPgmEff - hours;
                    break;
                case "SOA":
                    res = consXComm.HhacqSoa - consXComm.HhcrrgSoaEff - hours;
                    break;
                case "PJM":
                    res = consXComm.HhacqPjm - consXComm.HhcrrgPjmEff - hours;
                    break;
                case "BUC":
                    res = consXComm.HhacqBuc - consXComm.HhcrrgBucEff - hours;
                    break;
                case "SYD":
                    res = consXComm.HhacqSyd - consXComm.HhcrrgSydEff - hours;
                    break;
                case "E**":
                    res = consXComm.HhacqGen - consXComm.HhcrrgGenEff - hours;
                    break;
                case "D**":
                    res = consXComm.HhacqGde - consXComm.HhcrrgGdeEff - hours;
                    break;
            }

            return res;
        }

        public async Task<ResponseBase<CrrgGrpCdlsResponse?>> GetGrpCdlsAsync(CrrgGrpCdlsRequest crrgGrpCdlsRequest)
        {
            var res = new CrrgGrpCdlsResponse();

            //mi prendo la risorsa se non viene passata
            if (string.IsNullOrWhiteSpace(crrgGrpCdlsRequest.CrrgCdl))
            {
                crrgGrpCdlsRequest.CrrgCdl = (await _dbContext.FlussoMaccs.FirstOrDefaultAsync(x => x.MaccCMatricola == crrgGrpCdlsRequest.CrrgCRis)).MaccCCdl;
            }

            decimal? GrpCdlEff;
            decimal? GrpCdlPrev = null;

            // Valorizzazione CrrgGrpcdlEff
            var comm = await _dbContext.VsPpCommAperteXClis.FirstOrDefaultAsync(x => x.OrpbTstDoc == crrgGrpCdlsRequest.CrrgTstDoc && x.OrpbPrfDoc == crrgGrpCdlsRequest.CrrgPrfDoc && x.OrpbADoc == crrgGrpCdlsRequest.CrrgADoc && x.OrpbNDoc == crrgGrpCdlsRequest.CrrgNDoc);
            var pscCo02 = await _dbContext.PscCo02s.FirstOrDefaultAsync(x => x.CRisorsa == crrgGrpCdlsRequest.CrrgCRis && x.IdDoc == comm.TbcpId);
            if (pscCo02 != null)
            {
                res.CrrgGrpcdlEff = pscCo02.Grpcdl;
            }
            else
            {
                var pscQual = await _dbContext.PscQuals.FirstOrDefaultAsync(x => x.CRisorsa == crrgGrpCdlsRequest.CrrgCRis && x.TSoggetto == "C" && x.CSoggetto == comm.TbcpCCli && x.CDitta == CommonCostants.CDitta);

                if (pscQual != null)
                {
                    res.CrrgGrpcdlEff = pscQual.Grpcdl;
                }
                else
                {
                    res.CrrgGrpcdlEff = (await _dbContext.FlussoTcdls.FirstOrDefaultAsync(x => x.TcdlCCdl == crrgGrpCdlsRequest.CrrgCdl && x.TcdlCDitta == CommonCostants.CDitta)).TcdlGrpcdl;
                }
            }


			// Valorizzazione CrrgGrpcdlPrev
			var pscCo01 = await _dbContext.PscCo01s.FirstOrDefaultAsync(x => x.IdDoc == comm.TbcpId && x.Grpcdl == res.CrrgGrpcdlEff && x.CDitta == CommonCostants.CDitta);
			if (pscCo01 != null)
            {
                res.CrrgGrpcdlPrev = res.CrrgGrpcdlEff;  //Tentativo 1
            }
            else
            {
				var mvxzz12E = await _dbContext.Mvxzz12s.FirstOrDefaultAsync(x => x.Cprfc == "grpcdl" && x.DescrizioneRidotta == "E**");
				var mvxzz12D = await _dbContext.Mvxzz12s.FirstOrDefaultAsync(x => x.Cprfc == "grpcdl" && x.DescrizioneRidotta == "D**");
				var tcdlDefault = (await _dbContext.FlussoTcdls.FirstOrDefaultAsync(x => x.TcdlCCdl == crrgGrpCdlsRequest.CrrgCdl && x.TcdlCDitta == CommonCostants.CDitta)).TcdlGrpcdl;
				var mvxzz12Default = await _dbContext.Mvxzz12s.FirstOrDefaultAsync(x => x.Cprfc == "grpcdl" && x.Cod == tcdlDefault);
				Mvxzz12? macroGrpDefault = null;
				Mvxzz12? macroGrpDefaultOther = null;
				if ("SOA SYD".Contains(mvxzz12Default.DescrizioneRidotta)) { macroGrpDefault = mvxzz12E; macroGrpDefaultOther = mvxzz12D; }
				if ("PGM PJM BUC".Contains(mvxzz12Default.DescrizioneRidotta)) { macroGrpDefault = mvxzz12D; macroGrpDefaultOther = mvxzz12E; }
                if (macroGrpDefault == null || macroGrpDefaultOther == null) { return res; };

				var vsConsXComm = await _dbContext.VsConsXComms.FirstOrDefaultAsync(x => x.TbcpId == comm.TbcpId);
				if (vsConsXComm != null && ((macroGrpDefault.DescrizioneRidotta == "E**" && vsConsXComm.TfattGen != null)  || (macroGrpDefault.DescrizioneRidotta == "D**" && vsConsXComm.TfattGde != null))) 
                {
                    res.CrrgGrpcdlPrev= macroGrpDefault.Cod; //Tentativo 2
				} else
                {
					if (vsConsXComm != null && ((macroGrpDefault.DescrizioneRidotta == "E**" && vsConsXComm.TfattGde != null) || (macroGrpDefault.DescrizioneRidotta == "D**" && vsConsXComm.TfattGen != null)))
                    {
						res.CrrgGrpcdlPrev = macroGrpDefaultOther.Cod; //Tentativo 3
					}
				}
			}

            return ResponseBase<CrrgGrpCdlsResponse>.Success(res);


            //Eng : SOA SYD E**
            //Deli: PGM PJM BUC D**


            //var pscCo01 = await _dbContext.PscCo01s.FirstOrDefaultAsync(x => x.IdDoc == comm.TbcpId && x.Grpcdl == GrpCdlEff && x.CDitta == CommonCostants.CDitta);

            //if (pscCo01 != null)
            //{
            //    GrpCdlPrev = GrpCdlEff;
            //}
            //else
            //{
            //    var comm1 = await _dbContext.VsConsXComms.FirstOrDefaultAsync(x => x.TbcpId == comm.TbcpId);

            //    if (comm1 != null)
            //    {
            //        if (comm1.TfattGen != null)
            //        {
            //            var mvx = await _dbContext.Mvxzz12s.FirstOrDefaultAsync(x => x.Cprfc == "grpcdl" && x.DescrizioneRidotta == "E**");

            //            if (mvx != null)
            //            {
            //                GrpCdlPrev = mvx.Cod;
            //            }
            //        }


            //    }
            //}
        }

        public async Task<ResponseBase<List<FlussoCrrg?>>> GetConsuntiviByIslAsync(string isl)
        {
            var res = await _dbContext.FlussoCrrgs
                .Where(c => c.CrrgRifCliente == isl)
                .OrderByDescending(c => c.CrrgDttIni)
                .ToListAsync();

            return ResponseBase<List<FlussoCrrg?>>.Success(res);
        }
    }
}

