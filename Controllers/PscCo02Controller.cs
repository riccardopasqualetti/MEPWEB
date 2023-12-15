using Mep01Web.Infrastructure;
using Mep01Web.Service.Impl;
using Mep01Web.Service.Interface;
using MepWeb.DTO.Request;
using MepWeb.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MepWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PscCo02Controller:ControllerBase
    {
        private readonly SataconsultingContext _db;
        private readonly IPscCo02Service _pscCo02Service;
        public PscCo02Controller(SataconsultingContext sataconsulting, IPscCo02Service pscCo02Service)
        {
            _db = sataconsulting;
            _pscCo02Service = pscCo02Service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRecordAsync(PscCo02CreateRequest createRequest)
        {
            try
            {
                var response = await _pscCo02Service.CreateRecordAsync(createRequest);

                if (response.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return Problem(
                          detail: response.Errors[response.Errors.Count - 1].Code + " | " + response.Errors[response.Errors.Count - 1].Message,
                        statusCode: response.Errors[response.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
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
        public async Task<IActionResult> UpdateRecordAsync(PscCo02UpdateRequest updateRequest)
        {
            try
            {
                var response = await _pscCo02Service.UpdateRecordAsync(updateRequest);

                if (response.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return Problem(
                          detail: response.Errors[response.Errors.Count - 1].Code + " | " + response.Errors[response.Errors.Count - 1].Message,
                        statusCode: response.Errors[response.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
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
        [HttpDelete]
        public async Task<IActionResult> DeleteRecordAsync(string cRisorsa, string cDitta)
        {
            try
            {
                var response = await _pscCo02Service.DeleteRecordAsync(cRisorsa,cDitta);

                if (response.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return Problem(
                          detail: response.Errors[response.Errors.Count - 1].Code + " | " + response.Errors[response.Errors.Count - 1].Message,
                        statusCode: response.Errors[response.Errors.Count - 1].Code == "-2" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
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
