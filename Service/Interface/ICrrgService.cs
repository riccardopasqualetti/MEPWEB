using Mep01Web.DTO;
using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.Models;
using MepWeb.DTO.Response;

namespace Mep01Web.Service.Interface
{
    public interface ICrrgService
    {
        Task<ResponseBase<List<ConsXCommResponse>>?> GetAllConsAsync();
        Task<ResponseBase<CrrgResponse>?> GetCrrgAsync(CrrgCreateRequest crrgRequest);
        Task<ResponseBase<CrrgResponse>?> AddCrrgAsync(CrrgCreateRequest crrgRequest);
        Task<ResponseBase<CrrgResponse>?> DeleteCrrgAsync(CrrgCreateRequest crrgRequest);
        Task<CrrgCreateRequest> AddCrrgPrepareDataAsync(CrrgCreateRequest crrgCreateRequest);
        Task<CrrgCreateRequest> DeleteCrrgPrepareDataAsync(CrrgCreateRequest crrgCreateRequest);
        Task<decimal> CheckHoursAvailability(decimal idDoc, decimal hours, string qualifica);
    }
}
