using Mep01Web.DTO.Request;
using Mep01Web.Infrastructure;
using Mep01Web.Service.Interface;
using MepWeb.Costants;
using MepWeb.Service.Interface;
using MepWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MepWeb.DTO.Response;

namespace MepWeb.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CrrgApiController : ControllerBase
    {

        private readonly ICrrgService _crrgService;
        private readonly UserScope _userScope;

        public CrrgApiController(ICrrgService crrgService, UserScope userScope)
        {
            _crrgService = crrgService;
            _userScope = userScope;
        }
        
        [HttpPost("Create")]
        public async Task<ActionResult> Create(CrrgCreateRequest obj)
        {
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
				if (addCrrgResponse.Errors[0].Code == CrrgCreateErrors.CrrgApp)
				{
					ModelState.AddModelError("CrrgApp", addCrrgResponse.Errors[0].Message);
				}
				if (addCrrgResponse.Errors[0].Code == CrrgCreateErrors.CrrgMod)
				{
					ModelState.AddModelError("CrrgMod", addCrrgResponse.Errors[0].Message);
				}

				obj.Succeeded = "N";
                var res = new CrrgErrorsResponse
                {
                    Succeeded = "N"
                };

                foreach (var key in ModelState.Keys)
                {
                    if (ModelState[key].Errors.Count > 0)
                    {
                        var errorInfo = new Dictionary<string, string>();
                        errorInfo.Add(key, ModelState[key].Errors[0].ErrorMessage);
                        res.Errors.Add(errorInfo);
                    }
                }

                return Ok(res);
            }
            else
            {
                obj.Succeeded = "S";
            }            
            
            return Ok(obj);
        }

        [HttpGet("GetAllByIsl/{isl}")]
        public async Task<IActionResult> GetConsuntiviByIsl(string isl)
        {
            var res = await _crrgService.GetConsuntiviByIslAsync(isl);
                
            if (!res.Succeeded)
            {
                return Problem(
                detail: res.Errors[0].Message,
                statusCode: StatusCodes.Status500InternalServerError
                );
            }

            return Ok(res.Body);
        }

        //GET Delete
        [HttpDelete("Delete/{crrgCSrl}")]
        public async Task<IActionResult> Delete(int crrgCSrl)
        {
            CrrgCreateRequest obj = new CrrgCreateRequest((decimal)crrgCSrl);
            await _crrgService.DeleteCrrgPrepareDataAsync(obj);
            var res = await _crrgService.DeleteCrrgAsync(obj);

            if (!res.Succeeded)
            {
                return Problem(
                detail: res.Errors[0].Message,
                statusCode: StatusCodes.Status500InternalServerError
                );
            }

            return Ok(new { Response = "Eliminazione completata" });
        }
    }
}
