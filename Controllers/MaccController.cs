using MepWeb.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MepWeb.Controllers
{
	[Authorize]
	[Route("api/MepWeb_[controller]")]
	[ApiController]
	public class MaccController : ControllerBase
	{
		private readonly IMaccService _maccService;
		private readonly ILogger<MaccController> _logger;
		public MaccController(IMaccService maccService, ILogger<MaccController> logger)
		{
			_maccService = maccService;
			_logger = logger;

		}

		[HttpGet()]
		public async Task<IActionResult> GetAllAllowedMaccAsync()
		{
			_logger.Log(LogLevel.Information, "Ricevuta nuova richiesta GetAllAllowedMaccAsync");

			try
			{
				var getResponse = await _maccService.GetAllAllowedMaccAsync();

				if (getResponse.Succeeded)
				{
					return Ok(getResponse.Body);
				}
				else
				{
					return Problem(
						  detail: getResponse.Errors[getResponse.Errors.Count - 1].Code + " | " + getResponse.Errors[getResponse.Errors.Count - 1].Message,
						statusCode: getResponse.Errors[getResponse.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
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
