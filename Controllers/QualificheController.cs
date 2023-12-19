using MepWeb.DTO.Request;
using MepWeb.Service.Impl;
using MepWeb.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MepWeb.Controllers
{
    [Authorize]
    [Route("api/MepWeb_[controller]")]
    [ApiController]
    public class QualificheController : ControllerBase
    {
        private readonly IQualificheService _QualificheService;
        private readonly ILogger<QualificheController> _logger;
        public QualificheController(IQualificheService qualificheService, ILogger<QualificheController> logger)
        {
            _QualificheService = qualificheService;
            _logger = logger;   

        }
        [HttpGet()]
        public async Task<IActionResult> GetAllFromMvxzz12Async()
        {
            _logger.Log(LogLevel.Information, "Ricevuta nuova richiesta GetAllFromMvxzz12Async");

            try
            {
                var getResponse = await _QualificheService.GetAllFromMvxzz12Async();

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
