using Mep01Web.DTO;
using MepWeb.DTO.Request;
using MepWeb.DTO.Response;

namespace MepWeb.Service.Interface
{
    public interface IRegistroRicaricheService
    {
        Task<ResponseBase<RegistroRicarichePagedResponse?>> GetAllRecordsByIdDocPagedAsync(decimal idDoc, BasePagedRequest pagedRequest);
		Task<ResponseBase<List<RegistroRicaricheResponse?>>> GetAllRecordsByIdDocAsync(decimal idDoc);
        Task<ResponseBase<RegistroRicaricheResponse?>> GetSingleRecordAsync(decimal id);
        Task<ResponseBase<RegistroRicaricheResponse?>> CreateRecordAsync(RegistroRicaricheCreateRequest createRequest);
        Task<ResponseBase<RegistroRicaricheResponse?>> UpdateRecordAsync(RegistroRicaricheUpdateRequest updateRequest);
        Task<ResponseBase<RegistroRicaricheResponse?>> DeleteRecordAsync(decimal id);
    }
}
