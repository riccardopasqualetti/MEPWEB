using Mep01Web.DTO;
using MepWeb.DTO.Response;

namespace MepWeb.Service.Interface
{
	public interface IMaccService
	{
		Task<ResponseBase<List<MaccResponse>>> GetAllMaccAsync();
		Task<ResponseBase<List<MaccResponse>>> GetAllAllowedMaccAsync();
		Task<ResponseBase<MaccResponse>> GetMaccByCdlAsync(string cdl);
		Task<ResponseBase<MaccResponse>> GetMaccByRisFirstAsync(string risorsa);

	}
}
