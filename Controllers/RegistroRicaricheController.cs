using MepWeb.DTO.Request;
using MepWeb.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MepWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroRicaricheController : ControllerBase
    {
        private readonly IRegistroRicaricheService _registroRicaricheService;
        private readonly ILogger<OreQualificaController> _logger;

        public RegistroRicaricheController(IRegistroRicaricheService registroRicaricheService, ILogger<OreQualificaController> logger)
        {
            _registroRicaricheService = registroRicaricheService;
            _logger = logger;
        }

        [HttpGet("{idDocumento}")]
        public async Task<IActionResult> GetAllPscCo01sAsync(decimal idDoc)
        {
            _logger.Log(LogLevel.Information, "Ricevuta nuova richiesta GetAllRecordsByIdDocAsync");

            try
            {
                var getResponse = await _registroRicaricheService.GetAllRecordsByIdDocAsync(idDoc);

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSinglePscCo01Async(decimal id)
        {

            _logger.Log(LogLevel.Information, "Ricevuta nuova richiesta GetSingleRecordAsync");

            try
            {
                var getResponse = await _registroRicaricheService.GetSingleRecordAsync(id);

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
        public async Task<IActionResult> CreatePscCo01Async(RegistroRicaricheCreateRequest createRequest)
        {
            _logger.Log(LogLevel.Information, "Ricevuta nuova richiesta CreateRecordAsync");

            try
            {
                var getResponse = await _registroRicaricheService.CreateRecordAsync(createRequest);

                if (getResponse.Succeeded)
                {
                    return Ok();
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
        public async Task<IActionResult> UpdateTimeSchedule(RegistroRicaricheUpdateRequest updateRequest)
        {
            _logger.Log(LogLevel.Information, "Ricevuta nuova richiesta UpdateRecordAsync");

            try
            {
                var getResponse = await _registroRicaricheService.UpdateRecordAsync(updateRequest);

                if (getResponse.Succeeded)
                {
                    return Ok();
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeSchedule(decimal id)
        {
            _logger.Log(LogLevel.Information, "Ricevuta nuova richiesta DeleteRecordAsync");

            try
            {
                var getResponse = await _registroRicaricheService.DeleteRecordAsync(id);

                if (getResponse.Succeeded)
                {
                    return Ok();
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
