using Mep01Web.DTO;
using MepWeb.DTO.Response;

namespace MepWeb.Service.Interface
{
	public interface IVsCommAperteXCliService
	{
		Task<ResponseBase<List<CliXOpenCommResponse>>> GetCustOfOpenCommAsync();
		Task<ResponseBase<List<OpenCommByCustResponse>>> GetOpenCommByCustAsync(string codiceCliente);
		Task<ResponseBase<List<OpenCommByCustResponse>>> GetAllOpenComm();

    }
}
