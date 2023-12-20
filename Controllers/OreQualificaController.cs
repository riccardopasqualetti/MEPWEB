using Mep01Web.Controllers;
using Mep01Web.DTO.Request;
using Mep01Web.Infrastructure;
using MepWeb.DTO.Request;
using MepWeb.Service;
using MepWeb.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MepWeb.Controllers
{
    [Authorize]
    [Route("api/MepWeb_[controller]")]
    [ApiController]
    public class OreQualificaController : ControllerBase
    {
        private readonly IOreQualificaService _oreQualificaService;
        private readonly ILogger<OreQualificaController> _logger;
		private readonly SataconsultingContext _dbContext;


		public OreQualificaController(IOreQualificaService oreQualificaService, ILogger<OreQualificaController> logger, SataconsultingContext dbContext)
        {
            _oreQualificaService = oreQualificaService;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{idDocumento}/GetAllPaged")]
        public async Task<IActionResult> GetAllPscCo01sPagedAsync(decimal idDocumento, [FromQuery] BasePagedRequest request)
        {
            _logger.Log(LogLevel.Information, "Ricevuta nuova richiesta GetAllPscCo01sPagedAsync");

            try
            {
                var getResponse = await _oreQualificaService.GetAllRecordsByIdDocPagedAsync(idDocumento, request);

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

        [HttpGet("{idDocumento}")]
        public async Task<IActionResult> GetAllPscCo01sAsync(decimal idDocumento)
        {
            _logger.Log(LogLevel.Information, "Ricevuta nuova richiesta GetAllRecordsByIdDocAsync");

            try
            {
                var getResponse = await _oreQualificaService.GetAllRecordsByIdDocAsync(idDocumento);

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

        [HttpGet("{idDocumento}/{qualifica}")]
        public async Task<IActionResult> GetSinglePscCo01Async(decimal idDocumento, decimal qualifica)
        {

            _logger.Log(LogLevel.Information, "Ricevuta nuova richiesta GetSingleRecordAsync");

            try
            {
                var getResponse = await _oreQualificaService.GetSingleRecordAsync(idDocumento, qualifica);

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

        [HttpPost]
        public async Task<IActionResult> CreatePscCo01Async(OreQualificaCreateRequest createRequest)
        {
            _logger.Log(LogLevel.Information, "Ricevuta nuova richiesta CreateRecordAsync");

            try
            {
                var getResponse = await _oreQualificaService.CreateRecordAsync(createRequest);

                if (getResponse.Succeeded)
                {
                    return Ok(new { Response = "Operazione completata con successo"});
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

        [HttpPut]
        public async Task<IActionResult> UpdatePsCo01Async(OreQualificaUpdateRequest updateRequest)
        {
            _logger.Log(LogLevel.Information, "Ricevuta nuova richiesta UpdateRecordAsync");

			try
            {
                var getResponse = await _oreQualificaService.UpdateRecordAsync(updateRequest);

                if (getResponse.Succeeded)
                {
                    return Ok(new { Response = "Operazione completata con successo" });
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

        [HttpDelete("{idDocumento}/{qualifica}")]
        public async Task<IActionResult> DeleteTimeSchedule(decimal idDocumento, decimal qualifica)
        {
            _logger.Log(LogLevel.Information, "Ricevuta nuova richiesta DeleteRecordAsync");

            try
            {
                var getResponse = await _oreQualificaService.DeleteRecordAsync(idDocumento, qualifica);

                if (getResponse.Succeeded)
                {
                    return Ok(new { Response = "Operazione completata con successo" });
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
