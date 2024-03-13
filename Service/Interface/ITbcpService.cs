using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.DTO;
using MepWeb.DTO.Request;

namespace Mep01Web.Service.Interface
{
    public interface ITbcpService
    {
        Task<ResponseBase<TbcpResponse>?> GetTbcpByCodeAsync(TbcpGetRequest tbcpRequest);

        Task<ResponseBase<TbcpResponse>?> GetTbcpByCompCodeAsync(TbcpGetRequest tbcpRequest);

        Task<ResponseBase<List<TbcpResponse>>?> GetTbcpAllByCliAsync(TbcpGetRequest tbcpGetRequest);

        Task<ResponseBase<List<TbcpLightResponse>>?> GetTbcpAllLightByCliAsync(TbcpGetRequest tbcpGetRequest);

        Task<ResponseBase<TbcpLightResponse>> GetTbcpLightByNumAsync(TbcpGetRequest tbcpGetRequest);
        Task<ResponseBase<TbcpUpdateCampiOpzionaliRequest>> UpdateCampiOpzionaliAsync(TbcpUpdateCampiOpzionaliRequest request);
    }
}
