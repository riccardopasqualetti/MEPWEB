using Microsoft.AspNetCore.Mvc;
using Mep01Web.Infrastructure;
using Mep01Web.DTO.Request;
using Mep01Web.Service.Interface;

namespace Mep01Web.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class TatvController : ControllerBase
	{
		private readonly SataconsultingContext _db;
        private readonly ITatvService _tatvService;

        public TatvController(SataconsultingContext sataconsulting, ITatvService tatvService)
		{
			_db = sataconsulting;
			_tatvService = tatvService;
        }


		//GET
		// con [Route("api/[controller]")] in testa
		// usare:
		// https://localhost:44300/api/Tatv/GetISLByCodeAsync/ISL-0108-1000
		[HttpGet("GetISLByCodeAsync/{codeISL}")]
		public async Task<IActionResult> GetISLByCodeAsync(string codeISl)
        {
            try
			{
				var tatvGetRequest = new TatvGetRequest() { codeISl = codeISl };
                var getTatvResponse = await _tatvService.GetTatvAsync(tatvGetRequest);

				if (getTatvResponse.Succeeded)
				{
					return Ok(getTatvResponse.Body);
				}
				else
				{
					return Problem(
  						detail: getTatvResponse.Errors[getTatvResponse.Errors.Count - 1].Code + " | " + getTatvResponse.Errors[getTatvResponse.Errors.Count - 1].Message,
						statusCode: getTatvResponse.Errors[getTatvResponse.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
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
