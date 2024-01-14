using Mep01Web.DTO.Request;
using Mep01Web.Infrastructure;
using Mep01Web.Service.Interface;
using MepWeb.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MepWeb.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class VsCommAperteXCliController : ControllerBase
	{
		private readonly SataconsultingContext _dbContext;
		private readonly IVsCommAperteXCliService _vsCommAperteXCliService;

		public VsCommAperteXCliController(SataconsultingContext dbContext, ITbcpService tbcpService, IVsCommAperteXCliService vsCommAperteXCliService)
		{
			_dbContext = dbContext;
			_vsCommAperteXCliService = vsCommAperteXCliService;
		}

		[HttpGet("GetCommAllByCliAsync/{commCCli}")]
		public async Task<IActionResult> GetCommAllByCliAsync(string? commCCli = null)
		{
			try
			{
				var res = await _vsCommAperteXCliService.GetOpenCommByCustAsync(commCCli);

				if (res.Succeeded)
				{
					return Ok(res.Body);
				}
				else
				{
					return Problem(
  						detail: res.Errors[res.Errors.Count - 1].Code + " | " + res.Errors[res.Errors.Count - 1].Message,
						statusCode: res.Errors[res.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
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

        [HttpGet("GetAllComm")]
        public async Task<IActionResult> GetAllOpenComm()
        {
            try
            {
                var res = await _vsCommAperteXCliService.GetAllOpenComm();

                if (res.Succeeded)
                {
                    return Ok(res.Body);
                }
                else
                {
                    return Problem(
                          detail: res.Errors[res.Errors.Count - 1].Code + " | " + res.Errors[res.Errors.Count - 1].Message,
                        statusCode: res.Errors[res.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
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
