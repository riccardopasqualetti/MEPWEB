using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Mep01Web.Service.Impl;
using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.Service.Interface;

namespace Mep01Web.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class TbcpController : ControllerBase
	{
		private readonly SataconsultingContext _db;
        private readonly ITbcpService _tbcpService;

        public TbcpController(SataconsultingContext sataconsulting, ITbcpService tbcpService)
		{
			_db = sataconsulting;
            _tbcpService = tbcpService;
        }


        //GET
        // con [Route("api/[controller]")] in testa
        // usare:
        // https://localhost:44300/api/Tbcp/GetCommByCodeAsync?commTstDoc=COV&commPrfDoc=E&commADoc=2021&commNDoc=215007
        [HttpGet("GetCommByCodeAsync")]
		public async Task<IActionResult> GetCommByCodeAsync(string commTstDoc, string commPrfDoc, int commADoc, int commNDoc)
		{
			try
			{
				var getTbcpRequest = new TbcpGetRequest() { CommTstDoc = commTstDoc, CommPrfDoc = commPrfDoc, CommADoc = commADoc, CommNDoc = commNDoc };
				var getTbcpResponse = await _tbcpService.GetTbcpByCodeAsync(getTbcpRequest);

				if (getTbcpResponse.Succeeded)
				{
					return Ok(getTbcpResponse.Body);
				}
				else
				{
					return Problem(
  						detail: getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Code + " | " + getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Message,
						statusCode: getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
					);
				}
			}
			catch (Exception ex)
			{
				return Problem(
				detail: ex.Message,
				statusCode: StatusCodes.Status500InternalServerError
				);
			}
        }

        //GET
        // con [Route("api/[controller]")] in testa
        // usare:
        // https://localhost:44300/api/Tbcp/GetCommByCompCodeAsync/COVE2021215007
        [HttpGet("GetCommByCompCodeAsync/{commCompCode}")]
		public async Task<IActionResult> GetCommByCompCodeAsync(string commCompCode)
		{
            try
            {
                var getTbcpRequest = new TbcpGetRequest() { CommCompCode = commCompCode};
                var getTbcpResponse = await _tbcpService.GetTbcpByCompCodeAsync(getTbcpRequest);

				if (getTbcpResponse.Succeeded)
				{
					return Ok(getTbcpResponse.Body);
				}
				else
				{
					return Problem(
  						detail: getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Code + " | " + getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Message,
						statusCode: getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
					);
				}
			}

            catch (Exception ex)
            {
                return Problem(
					detail: ex.Message,
					statusCode: StatusCodes.Status500InternalServerError
                );
            }
 		}

		//GET
		// con [Route("api/[controller]")] in testa
		// usare:
		// https://localhost:44300/api/Tbcp/GetCommByCompCode1Async/COV/E/2021/215007
		[HttpGet("GetCommByCompCode1Async/{commTstDoc}/{commPrfDoc}/{commADoc}/{commNDoc}")]
		public async Task<IActionResult> GetCommByCompCode1Async(string commTstDoc, string commPrfDoc, int commADoc, int commNDoc)
		{
			try
			{
				var getTbcpRequest = new TbcpGetRequest() { CommTstDoc = commTstDoc, CommPrfDoc = commPrfDoc, CommADoc = commADoc, CommNDoc = commNDoc };
				var getTbcpResponse = await _tbcpService.GetTbcpByCodeAsync(getTbcpRequest);

				if (getTbcpResponse.Succeeded)
				{
					return Ok(getTbcpResponse.Body);
				}
				else
				{
					return Problem(
  						detail: getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Code + " | " + getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Message,
						statusCode: getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
					);
				}
			}

			catch (Exception ex)
			{
				return Problem(
					detail: ex.Message,
					statusCode: StatusCodes.Status500InternalServerError
				);
			}
		}

		//GET
		// con [Route("api/[controller]")] in testa
		// usare:
		// https://localhost:44300/api/Tbcp/GetCommAllByCliAsync
		[HttpGet("GetCommAllByCliAsync")]
		// https://localhost:44300/api/Tbcp/GetCommAllByCliAsync/0153S100
		[HttpGet("GetCommAllByCliAsync/{commCCli}")]
		public async Task<IActionResult> GetCommAllByCliAsync(string? commCCli = null)
		{
			try
			{				
				var getTbcpRequest = new TbcpGetRequest() { CommCCli = commCCli };
				var getTbcpResponse = await _tbcpService.GetTbcpAllLightByCliAsync(getTbcpRequest);

				if (getTbcpResponse.Succeeded)
				{
					return Ok(getTbcpResponse.Body);
				}
				else
				{
					return Problem(
  						detail: getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Code + " | " + getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Message,
						statusCode: getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
					);
				}
			}

			catch (Exception ex)
			{
				return Problem(
					detail: ex.Message,
					statusCode: StatusCodes.Status500InternalServerError
				);
			}
		}


        //GET
        // con [Route("api/[controller]")] in testa
        // usare:
        // https://localhost:44300/api/Tbcp/GetCommByNumAsync/096003
        [HttpGet("GetCommByNumAsync/{commNDoc}")]
        public async Task<IActionResult> GetCommByNumAsync(string? commNDoc = null)
        {
            try
            {
                var getTbcpRequest = new TbcpGetRequest() { CommNDoc = Convert.ToDecimal(commNDoc) };
                var getTbcpResponse = await _tbcpService.GetTbcpLightByNumAsync(getTbcpRequest);

                if (getTbcpResponse.Succeeded)
                {
                    return Ok(getTbcpResponse.Body);
                }
                else
                {
                    return Problem(
                          detail: getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Code + " | " + getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Message,
                        statusCode: getTbcpResponse.Errors[getTbcpResponse.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
                    );
                }
            }

            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

    }

}
