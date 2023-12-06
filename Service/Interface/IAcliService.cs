using Mep01Web.DTO;
using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;

namespace Mep01Web.Service.Interface
{
    public interface IAcliService
    {
        Task<ResponseBase<AcliResponse>?> GetAcliAsync(AcliGetRequest acliRequest);

        Task<ResponseBase<List<AcliResponse>>?> GetAcliAllAsync();
    }
}
