using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using MepWeb.DTO.Response;
using MepWeb.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace MepWeb.Service.Impl
{
	public class Psc001AService : IPsc001AService
	{
		private readonly SataconsultingContext _context;

		public Psc001AService(SataconsultingContext context)
		{
			_context = context;
		}

		public async Task<ResponseBase<List<TotOreVerbalizzatePerQualificaResponse?>>> GetTotOreVerbaliByCommessaAsync(string ncommessa)
		{
			var query = await (from p in _context.Psc001as
							   where EF.Functions.Like(p.RifInterno, $"%{ncommessa}")
							   group new { p.QualRespInt, TotalHh = p.HhIntTot } by p.QualRespInt into g
							   select new TotOreVerbalizzatePerQualificaResponse {
								   Qualifica = g.Key,
								   TotaleOre = g.Sum(x => x.TotalHh) 
							   }).ToListAsync();

			return query;
        }

//		SELECT QUAL_RESP_INT, SUM(HH_INT_TOT) HH_VERBALIZZATE
//		FROM DBA.PSC_001A
//		WHERE RIF_INTERNO LIKE '%212039'
//		GROUP BY QUAL_RESP_INT

    }
}
