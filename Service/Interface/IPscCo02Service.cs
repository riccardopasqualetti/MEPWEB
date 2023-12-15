using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.DTO;
using Mep01Web.Models;
using MepWeb.DTO.Request;
using MepWeb.DTO.Response;

namespace MepWeb.Service.Interface
{
    public interface IPscCo02Service
    {
        Task<ResponseBase<PscCo02PagedResponse?>> GetAllFromPscCo02PagedAsync(decimal idDoc, BasePagedRequest pagedRequest);
		Task<ResponseBase<List<PscCo02Response>?>> GetAllFromPscCo02Async( decimal idDoc);
        Task<ResponseBase<PscCo02Response?>> GetSingleRecordAsync(string cRisorsa,decimal idDoc);
        Task<ResponseBase<PscCo02?>> CreateRecordAsync(PscCo02CreateRequest request);
        Task<ResponseBase<PscCo02?>> UpdateRecordAsync(PscCo02UpdateRequest request);
        Task <ResponseBase<PscCo02?>> DeleteRecordAsync(decimal idDoc, string cRisorsa);
    }
}
