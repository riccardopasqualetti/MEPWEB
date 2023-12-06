using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Microsoft.EntityFrameworkCore;
using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.DTO;
using Mep01Web.Service.Interface;

namespace Mep01Web.Service.Impl
{
    public class AcliService : IAcliService
	{
		private readonly SataconsultingContext _db;

		public AcliService(SataconsultingContext dbContext)
		{
			_db = dbContext;
		}

		public async Task<ResponseBase<AcliResponse>?> GetAcliAsync(AcliGetRequest acliGetRequest)
		{
			FlussoAcli flussoAcli = await _db.FlussoAclis.SingleOrDefaultAsync(e => e.AcliAEsercizio == acliGetRequest.AcliAEsercizio && e.AcliCCliente == acliGetRequest.AcliCCliente && e.AcliCDitta == acliGetRequest.AcliCDitta);

			if (flussoAcli == null)
			{
				return ResponseBase<AcliResponse?>.Failed("-1", $"Non sono stati trovati record con questo valore '{acliGetRequest.AcliCCliente}'");
			}
			var acliResponse = new AcliResponse();
			acliResponse.FlussoAcli = flussoAcli;
			return ResponseBase<AcliResponse?>.Success(acliResponse);
		}

		public async Task<ResponseBase<List<AcliResponse>>?> GetAcliAllAsync()
		{					
			List<FlussoAcli> flussoAcliList = await _db.FlussoAclis.ToListAsync();

			if (flussoAcliList == null)
			{
				return ResponseBase<List<AcliResponse>?>.Failed("-2", $"Non sono stati trovati record in flusso_acli");
			}

			List<AcliResponse> acliResponseList = new List<AcliResponse>();
			foreach (var flussoAcli in flussoAcliList)
			{
				var acliResponse = new AcliResponse();
				acliResponse.FlussoAcli = flussoAcli;
				acliResponseList.Add(acliResponse);
			}
			return ResponseBase<List<AcliResponse>?>.Success(acliResponseList);
						
		}

	}

}
