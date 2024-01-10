using Mep01Web.Infrastructure;
using Mep01Web.Service.Interface;
using MepWeb.Service.Impl;
using MepWeb.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MepWeb.Controllers
{
	[Authorize]
	[Route("Crrg/api/[controller]")]
	[ApiController]
	public class VsPpMonitorIslController : ControllerBase
	{
		private readonly SataconsultingContext _dbContext;

		public VsPpMonitorIslController(SataconsultingContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet("GetAllComm")]
		public async Task<IActionResult> GetAllComm()
		{
			try
			{
				var res = await _dbContext.VsPpMonitorIsl.ToListAsync();

				return Ok(res);
			}

			catch (Exception ex)
			{
				return Problem(
					detail: ex.Message,
					statusCode: StatusCodes.Status500InternalServerError
				);
			}
		}

		[HttpPost("GetIslByStato")]
		public async Task<IActionResult> GetIslByStato(string[] flags)
		{
			try
			{
				var res = await _dbContext.VsPpMonitorIsl.Where(x => flags.Contains(x.Flag)).Select(x => new { x.RifCli, x.Stato, x.Flag, x.AcliRagSoc1, x.TatvDesc }).ToListAsync();

				return Ok(res);
			}

			catch (Exception ex)
			{
				return Problem(
					detail: ex.Message,
					statusCode: StatusCodes.Status500InternalServerError
				);
			}
		}

		[HttpGet("GetByRifCli/{rifCli}")]
		public async Task<IActionResult> GetByIsl(string rifCli)
		{
			try
			{
				var res = await _dbContext.VsPpMonitorIsl.FirstOrDefaultAsync(x => x.RifCli == rifCli);

				return Ok(res);
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
