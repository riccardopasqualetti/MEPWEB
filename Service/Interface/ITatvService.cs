using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.DTO;

namespace Mep01Web.Service.Interface
{
    public interface ITatvService
    {
        Task<ResponseBase<TatvResponse?>> GetTatvAsync(TatvGetRequest tatvRequest);

    }
}
