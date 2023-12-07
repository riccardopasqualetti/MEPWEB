using Microsoft.AspNetCore.Mvc;
using Mep01Web.Infrastructure;
using Azure;
using Mep01Web.Service.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Mep01Web.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
	[ApiController]
	public class AcliController : ControllerBase
	{
		private readonly SataconsultingContext _db;
		private readonly IAcliService _acliService;
		public AcliController(SataconsultingContext sataconsulting, IAcliService acliService)
		{
			_db = sataconsulting;
			_acliService = acliService;
		}

		//GET
		// con [Route("api/[controller]")] in testa
		// usare:
		// https://localhost:44300/api/Acli/GetAcliAllAsync
		[HttpGet("GetAcliAllAsync")]
		public async Task<IActionResult> GetAcliAllAsync()
		{
			try
			{
				var getAcliAllResponse = await _acliService.GetAcliAllAsync();

				if (getAcliAllResponse.Succeeded)
				{
					return Ok(getAcliAllResponse.Body);
				}
				else
				{
					return Problem(
  						detail: getAcliAllResponse.Errors[getAcliAllResponse.Errors.Count - 1].Code + " | " + getAcliAllResponse.Errors[getAcliAllResponse.Errors.Count - 1].Message,
						statusCode: getAcliAllResponse.Errors[getAcliAllResponse.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
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
