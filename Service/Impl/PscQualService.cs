using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using MepWeb.Costants;
using MepWeb.DTO.Response;
using MepWeb.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MepWeb.Service.Impl
{
	public class PscQualService : IPscQualService
	{
		private readonly SataconsultingContext _dbContext;

		public PscQualService(SataconsultingContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ResponseBase<List<PscQualResponse>>> GetAllPscQual()
		{
			var pscs = await _dbContext.PscQuals.ToListAsync();
			var res = new List<PscQualResponse>();

			foreach (var pscQual in pscs)
			{
				res.Add(new PscQualResponse
				{
					TSoggetto = pscQual.TSoggetto,
					CSoggetto = pscQual.CSoggetto,
					TindProgressivo = pscQual.TindProgressivo,
					CRisorsa = pscQual.CRisorsa,
					Grpcdl = pscQual.Grpcdl,
					CDitta = pscQual.CDitta,
					UtenteIns = pscQual.UtenteIns,
					DtIns = pscQual.DtIns,
					UtenteUm = pscQual.UtenteUm,
					DtUm = pscQual.DtUm
				});
			}

			return res;
		}

		public async Task<ResponseBase<PscQualResponse>> GetPscQualByRis(string risorsa)
		{
			var psc = await _dbContext.PscQuals.FirstOrDefaultAsync(x => x.CRisorsa == risorsa);

			if (psc == null)
			{
				return ResponseBase<PscQualResponse>.Failed(PscCo02Errors.RecordNonTrovati, "Non sono stati trovati record con la risorsa indicata");
			}

			var res = new PscQualResponse
			{
				TSoggetto = psc.TSoggetto,
				CSoggetto = psc.CSoggetto,
				TindProgressivo = psc.TindProgressivo,
				CRisorsa = psc.CRisorsa,
				Grpcdl = psc.Grpcdl,
				CDitta = psc.CDitta,
				UtenteIns = psc.UtenteIns,
				DtIns = psc.DtIns,
				UtenteUm = psc.UtenteUm,
				DtUm = psc.DtUm
			};

			return ResponseBase<PscQualResponse>.Success(res);
		}
	}
}
