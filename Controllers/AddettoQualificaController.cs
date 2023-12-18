using Mep01Web.Infrastructure;
using Mep01Web.Service.Impl;
using Mep01Web.Service.Interface;
using MepWeb.DTO.Request;
using MepWeb.Service.Impl;
using MepWeb.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MepWeb.Controllers
{
    [Route("api/MepWeb_[controller]")]
    [ApiController]
    public class AddettoQualificaController:ControllerBase
    {
        private readonly SataconsultingContext _db;
        private readonly IPscCo02Service _pscCo02Service;
        public AddettoQualificaController(SataconsultingContext sataconsulting, IPscCo02Service pscCo02Service)
        {
            _db = sataconsulting;
            _pscCo02Service = pscCo02Service;
        }

		[HttpGet("GetAllPaged/{idDocumento}")]
		public async Task<IActionResult> GetAllPscCo01sPagedAsync(decimal idDoc, [FromQuery] BasePagedRequest request)
		{

			try
			{
				var getResponse = await _pscCo02Service.GetAllFromPscCo02PagedAsync(idDoc, request);

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
        public async Task<IActionResult> DeleteRecordAsync(decimal idDoc, string cRisorsa)
        {
            try
            {
                var response = await _pscCo02Service.DeleteRecordAsync(idDoc, cRisorsa);

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
        [HttpGet("{idDocumento}")]
        public async Task<IActionResult> GetAllPscCo02sAsync(decimal idDoc)
        {
            

            try
            {
                var getResponse = await _pscCo02Service.GetAllFromPscCo02Async(idDoc);

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
        [HttpGet("{cRisorsa}/{idDocumento}")]
        public async Task<IActionResult> GetSinglePscCo02Async(string cRisorsa, decimal idDoc)
        {

            try
            {
                var getResponse = await _pscCo02Service.GetSingleRecordAsync(cRisorsa, idDoc);

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
