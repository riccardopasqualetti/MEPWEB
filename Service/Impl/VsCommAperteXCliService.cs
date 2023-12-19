using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using MepWeb.DTO.Response;
using MepWeb.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MepWeb.Service.Impl
{
	public class VsCommAperteXCliService : IVsCommAperteXCliService
	{
		private readonly SataconsultingContext _dbContext;

		public VsCommAperteXCliService(SataconsultingContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ResponseBase<List<CliXOpenCommResponse>>> GetCustOfOpenCommAsync()
		{

			var res = _dbContext.VsPpCommAperteXClis
				.Where(x => x.TbcpCCli != null)
				.Select(x => new { x.CCliRag, x.TbcpCCli })
				.ToList()
				.GroupBy(x => new { x.CCliRag, x.TbcpCCli })
				.Select(x => new CliXOpenCommResponse
				{
					CCliRag = x.Key.CCliRag,
					TbcpCCli = x.Key.TbcpCCli,
				}).ToList();

			return ResponseBase<List<CliXOpenCommResponse>>.Success(res);
		}

		public async Task<ResponseBase<List<OpenCommByCustResponse>>> GetOpenCommByCustAsync(string codiceCliente)
		{
			var res = await _dbContext.VsPpCommAperteXClis
				.Where(x => x.TbcpCCli == codiceCliente)
				.Select(x => new OpenCommByCustResponse 
				{ 
					CommDescDd = x.CommDescDd,
					OrpbTstDoc = x.OrpbTstDoc,
					OrpbPrfDoc = x.OrpbPrfDoc,
					OrpbADoc = x.OrpbADoc,
					OrpbNDoc = x.OrpbNDoc
				})
				.ToListAsync();

			return ResponseBase<List<OpenCommByCustResponse>>.Success(res);
		}
	}
}
