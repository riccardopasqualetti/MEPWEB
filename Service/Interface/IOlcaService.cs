using Mep01Web.DTO;
using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.Models;

namespace Mep01Web.Service.Interface
{
    public interface IOlcaService
    {
		Task<ResponseBase<OlcaCitoResponse>?> GetOlcaCitoAsync(OlcaGetRequest olcaGetRequest);
    }
}
