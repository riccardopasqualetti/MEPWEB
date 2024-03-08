using Mep01Web.DTO;
using MepWeb.DTO.Response;

namespace MepWeb.Service.Interface
{
	public interface IPsc001AService
	{
		Task<ResponseBase<List<TotOreVerbalizzatePerQualificaResponse?>>> GetTotOreVerbaliByCommessaAsync(string ncommessa);
	}
}
