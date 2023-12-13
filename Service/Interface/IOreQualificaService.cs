using Mep01Web.DTO;
using MepWeb.DTO.Request;
using MepWeb.DTO.Response;

namespace MepWeb.Service.Interface
{
    public interface IOreQualificaService
    {
        Task<ResponseBase<List<OreQualificaResponse?>>> GetAllRecordsByIdDocAsync(decimal idDoc);
        Task<ResponseBase<OreQualificaResponse?>> GetSingleRecordAsync(decimal idDoc, decimal grpcdl);
        Task<ResponseBase<OreQualificaResponse?>> CreateRecordAsync(OreQualificaCreateRequest createRequest);
        Task<ResponseBase<OreQualificaResponse?>> UpdateRecordAsync(OreQualificaUpdateRequest updateRequest);
        Task<ResponseBase<OreQualificaResponse?>> DeleteRecordAsync(decimal idDoc, decimal grpcdl);
    }
}
