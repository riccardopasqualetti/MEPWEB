using Mep01Web.DTO;
using MepWeb.DTO.Response;

namespace MepWeb.Service.Interface
{
	public interface ITcdlService
	{
		Task<ResponseBase<List<TcdlResponse>>> GetAllTcdlAsync();
		Task<ResponseBase<TcdlResponse>> GetTcdlByCdl(string cCdl);
	}
}
