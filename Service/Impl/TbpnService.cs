using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Microsoft.EntityFrameworkCore;
using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.DTO;
using Newtonsoft.Json.Linq;
using Mep01Web.Service.Interface;

namespace Mep01Web.Service.Impl
{
    public class TbpnService : ITbpnService
    {
        private readonly SataconsultingContext _db;

        public TbpnService(SataconsultingContext dbContext)
        {
            _db = dbContext;
        }
		
		
        public async Task<ResponseBase<TbpnResponse>?> GetTbpnByCodeAsync(TbpnGetRequest tbpnGetRequest)
        {            
            FlussoTbpn? flussoTbpn = await _db.FlussoTbpns.SingleOrDefaultAsync(x => x.TbpnCPart == tbpnGetRequest.TbpnCPart && x.TbpnCDitta == tbpnGetRequest.TbpnCDitta);
            if (flussoTbpn == null)
            {
				return ResponseBase<TbpnResponse?>.Failed("-1", $"Non sono stati trovati record con questo valore '{tbpnGetRequest.TbpnCDitta}/{tbpnGetRequest.TbpnCPart}'");
			}

			var tbpnResponse = new TbpnResponse();

			tbpnResponse.FlussoTbpn = flussoTbpn;

			return ResponseBase<TbpnResponse?>.Success(tbpnResponse);
		}


		// Get all code starting by tbpnGetRequest.TbpnCPart 
		public async Task<ResponseBase<List<TbpnLightResponse>>?> GetTbpnLikeAllLightAsync(TbpnGetRequest tbpnGetRequest)
		{
			List<FlussoTbpn> flussoTbpnList;

			if (tbpnGetRequest.TbpnCDitta == null) { tbpnGetRequest.TbpnCDitta = "01"; };

			flussoTbpnList = await _db.FlussoTbpns.Where(x => x.TbpnCDitta == tbpnGetRequest.TbpnCDitta && x.TbpnCPart.StartsWith(tbpnGetRequest.TbpnCPart) && x.TbpnFlgObsoleto == "N").ToListAsync();

			if (flussoTbpnList == null)
			{
				return ResponseBase<List<TbpnLightResponse>?>.Failed("-2", $"Non sono stati trovati record in flusso_tbpn con codici che iniziano per '{tbpnGetRequest.TbpnCPart}'");
			}

			List<TbpnLightResponse> tbpnResponseList = new List<TbpnLightResponse>();
			foreach (var flussoTbpn in flussoTbpnList)
			{
				var tbpnResponse = new TbpnLightResponse();
				tbpnResponse.TbpnCPart = flussoTbpn.TbpnCPart;
				tbpnResponseList.Add(tbpnResponse);
			}

			return ResponseBase<List<TbpnLightResponse>?>.Success(tbpnResponseList);
		}


	}
}
