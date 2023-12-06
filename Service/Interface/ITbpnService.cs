using Mep01Web.DTO;
using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.Models.Views;

namespace Mep01Web.Service.Interface
{
	public interface ITbpnService
	{
	    Task<ResponseBase<TbpnResponse>?> GetTbpnByCodeAsync(TbpnGetRequest tbpnGetRequest);

		Task<ResponseBase<List<TbpnLightResponse>>?> GetTbpnLikeAllLightAsync(TbpnGetRequest tbpnGetRequest);
		}
}
