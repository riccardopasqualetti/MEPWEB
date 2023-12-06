using Mep01Web.DTO;
using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.Models;

namespace Mep01Web.Service.Interface
{
    public interface ICrrgService
    {
        Task<ResponseBase<CrrgResponse>?> GetCrrgAsync(CrrgCreateRequest crrgRequest);
        Task<ResponseBase<CrrgResponse>?> AddCrrgAsync(CrrgCreateRequest crrgRequest);
        Task<ResponseBase<CrrgResponse>?> DeleteCrrgAsync(CrrgCreateRequest crrgRequest);
        Task<CrrgCreateRequest> AddCrrgPrepareDataAsync(CrrgCreateRequest crrgCreateRequest);
        Task<CrrgCreateRequest> DeleteCrrgPrepareDataAsync(CrrgCreateRequest crrgCreateRequest);
    }
}
