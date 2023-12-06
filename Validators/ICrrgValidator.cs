using Mep01Web.DTO;
using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.Models;

namespace Mep01Web.Validators
{
    public interface ICrrgValidator
    {
        public Task<ResponseBase<CrrgResponse>?> CrrgValidateAsync(CrrgCreateRequest crrgRequest);
    }
}
