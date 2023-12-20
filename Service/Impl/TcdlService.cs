using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using MepWeb.Costants;
using MepWeb.DTO.Response;
using MepWeb.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MepWeb.Service.Impl
{
	public class TcdlService : ITcdlService
	{
		private readonly SataconsultingContext _dbContext;

		public TcdlService(SataconsultingContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ResponseBase<List<TcdlResponse>>> GetAllTcdlAsync()
		{
			var tcdls = await _dbContext.FlussoTcdls.ToListAsync();
			var res = new List<TcdlResponse>();

			foreach (var tcdl in tcdls)
			{
				res.Add(new TcdlResponse
				{
					TcdlCCdl = tcdl.TcdlCCdl,
					TcdlCDitta = tcdl.TcdlCDitta,
					TcdlDesc = tcdl.TcdlDesc,
					TcdlFlgSched = tcdl.TcdlFlgSched,
					TcdlCCale = tcdl.TcdlCCale,
					TcdlTDomina = tcdl.TcdlTDomina,
					TcdlFlgIe = tcdl.TcdlFlgIe,
					TcdlCCdc = tcdl.TcdlCCdc,
					TcdlFlgDett = tcdl.TcdlFlgDett,
					TcdlNEntProd = tcdl.TcdlNEntProd,
					TcdlHhPrevMese = tcdl.TcdlHhPrevMese,
					TcdlHhMedMese = tcdl.TcdlHhMedMese,
					TcdlHhTurno = tcdl.TcdlHhTurno,
					TcdlPercEff = tcdl.TcdlPercEff,
					TcdlTmAggSec = tcdl.TcdlTmAggSec,
					TcdlColor = tcdl.TcdlColor,
					TcdlDtIns = tcdl.TcdlDtIns,
					TcdlUtenteIns = tcdl.TcdlUtenteIns,
					TcdlDtUm = tcdl.TcdlDtUm,
					TcdlUtenteUm = tcdl.TcdlUtenteUm,
					TcdlTProp = tcdl.TcdlTProp,
					TcdlCProp = tcdl.TcdlCProp,
					TcdlFlgSchedMano = tcdl.TcdlFlgSchedMano,
					TcdlCGruppoSched = tcdl.TcdlCGruppoSched,
					TcdlCalgassris = tcdl.TcdlCalgassris,
					TcdlCalgschris = tcdl.TcdlCalgschris,
					TcdlDtUltimaSched = tcdl.TcdlDtUltimaSched,
					TcdlGrpcdl = tcdl.TcdlGrpcdl,
					TcdlCLivP = tcdl.TcdlCLivP,
					TcdlId = tcdl.TcdlId,
					TcdlCMagVers = tcdl.TcdlCMagVers,
					TcdlCMagPrel = tcdl.TcdlCMagPrel
				});
			}

			return ResponseBase<List<TcdlResponse>>.Success(res);
		}

		public async Task<ResponseBase<TcdlResponse>> GetTcdlByCdl(string cCdl)
		{
			var tcdl = await _dbContext.FlussoTcdls.FirstOrDefaultAsync(x => x.TcdlCCdl == cCdl);

			if (tcdl == null)
			{
				return ResponseBase<TcdlResponse>.Failed(PscCo02Errors.RecordNonTrovati, "Non è stato trovato il record con il codice specificato");
			}

			var res = new TcdlResponse
			{
				TcdlCCdl = tcdl.TcdlCCdl,
				TcdlCDitta = tcdl.TcdlCDitta,
				TcdlDesc = tcdl.TcdlDesc,
				TcdlFlgSched = tcdl.TcdlFlgSched,
				TcdlCCale = tcdl.TcdlCCale,
				TcdlTDomina = tcdl.TcdlTDomina,
				TcdlFlgIe = tcdl.TcdlFlgIe,
				TcdlCCdc = tcdl.TcdlCCdc,
				TcdlFlgDett = tcdl.TcdlFlgDett,
				TcdlNEntProd = tcdl.TcdlNEntProd,
				TcdlHhPrevMese = tcdl.TcdlHhPrevMese,
				TcdlHhMedMese = tcdl.TcdlHhMedMese,
				TcdlHhTurno = tcdl.TcdlHhTurno,
				TcdlPercEff = tcdl.TcdlPercEff,
				TcdlTmAggSec = tcdl.TcdlTmAggSec,
				TcdlColor = tcdl.TcdlColor,
				TcdlDtIns = tcdl.TcdlDtIns,
				TcdlUtenteIns = tcdl.TcdlUtenteIns,
				TcdlDtUm = tcdl.TcdlDtUm,
				TcdlUtenteUm = tcdl.TcdlUtenteUm,
				TcdlTProp = tcdl.TcdlTProp,
				TcdlCProp = tcdl.TcdlCProp,
				TcdlFlgSchedMano = tcdl.TcdlFlgSchedMano,
				TcdlCGruppoSched = tcdl.TcdlCGruppoSched,
				TcdlCalgassris = tcdl.TcdlCalgassris,
				TcdlCalgschris = tcdl.TcdlCalgschris,
				TcdlDtUltimaSched = tcdl.TcdlDtUltimaSched,
				TcdlGrpcdl = tcdl.TcdlGrpcdl,
				TcdlCLivP = tcdl.TcdlCLivP,
				TcdlId = tcdl.TcdlId,
				TcdlCMagVers = tcdl.TcdlCMagVers,
				TcdlCMagPrel = tcdl.TcdlCMagPrel
			};

			return ResponseBase<TcdlResponse>.Success(res);
		}
	}
}
