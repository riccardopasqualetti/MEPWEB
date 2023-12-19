using Mep01Web.DTO;
using MepWeb.DTO.Response;

namespace MepWeb.Service.Interface
{
	public interface IPscQualService
	{
		Task<ResponseBase<List<PscQualResponse>>> GetAllPscQual();
		Task<ResponseBase<PscQualResponse>> GetPscQualByRis(string risorsa);
	}
}
