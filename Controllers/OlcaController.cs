using Mep01Web.DTO.Request;
using Mep01Web.Infrastructure;
using Mep01Web.Service.Impl;
using Mep01Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Mep01Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OlcaController : ControllerBase
	{

		private readonly SataconsultingContext _db;
		private readonly IOlcaService _olcaService;

		public OlcaController(SataconsultingContext sataconsulting, IOlcaService olcaService)
		{
			_db = sataconsulting;
			_olcaService = olcaService;
		}

		//GET
		// con [Route("api/[controller]")] in testa
		// usare:
		// https://localhost:44300/api/Olca/GetOlcaCitoByCommAsync/COV/A/2023/231006
		[HttpGet("GetOlcaCitoByCommAsync/{commTstDoc}/{commPrfDoc}/{commADoc}/{commNDoc}")]
		public async Task<IActionResult> GetOlcaCitoByCommAsync(string commTstDoc, string commPrfDoc, int commADoc, int commNDoc)
		{
			try
			{
				var olcaGetRequest = new OlcaGetRequest() { OlcaTstDoc = commTstDoc, OlcaPrfDoc = commPrfDoc, OlcaADoc = commADoc, OlcaNDoc = commNDoc, OlcaNRigaDoc = 1 }; ;
				var getOlcaResponse = await _olcaService.GetOlcaCitoAsync(olcaGetRequest);

				if (getOlcaResponse.Succeeded)
				{
					return Ok(getOlcaResponse.Body);
				}
				else
				{
					return Problem(
  						detail: getOlcaResponse.Errors[getOlcaResponse.Errors.Count - 1].Code + " | " + getOlcaResponse.Errors[getOlcaResponse.Errors.Count - 1].Message,
						statusCode: getOlcaResponse.Errors[getOlcaResponse.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
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
