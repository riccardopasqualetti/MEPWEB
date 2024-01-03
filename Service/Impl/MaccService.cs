using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using MepWeb.Costants;
using MepWeb.DTO.Response;
using MepWeb.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace MepWeb.Service.Impl
{
	public class MaccService : IMaccService
	{
		private readonly SataconsultingContext _dbContext;

		public MaccService(SataconsultingContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ResponseBase<List<MaccResponse>>> GetAllMaccAsync()
		{
			var maccs = await _dbContext.FlussoMaccs.ToListAsync();
			var res = new List<MaccResponse>();

			foreach (var macc in maccs)
			{
				res.Add(new MaccResponse
				{
					MaccCCdl = macc.MaccCCdl,
					MaccTipo = macc.MaccTipo,
					MaccCMatricola = macc.MaccCMatricola,
					MaccCDitta = macc.MaccCDitta,
					MaccDesc = macc.MaccDesc,
					MaccPassw = macc.MaccPassw,
					MaccFlgMag = macc.MaccFlgMag,
					MaccFlgGrp = macc.MaccFlgGrp,
					MaccNRis = macc.MaccNRis,
					MaccGrpCosto = macc.MaccGrpCosto,
					MaccDtIns = macc.MaccDtIns,
					MaccUtenteIns = macc.MaccUtenteIns,
					MaccDtUm = macc.MaccDtUm,
					MaccUtenteUm = macc.MaccUtenteUm,
					MaccPortata = macc.MaccPortata,
					MaccId = macc.MaccId,
					MaccStris = macc.MaccStris,
					MaccFlgAttivo = macc.MaccFlgAttivo,
					MaccFlgPresidio = macc.MaccFlgPresidio,
					MaccListaCaus = macc.MaccListaCaus,
					MaccTprocpers = macc.MaccTprocpers,
					MaccFlgComdir = macc.MaccFlgComdir,
					MaccPathParprogramSrc = macc.MaccPathParprogramSrc,
					MaccPathParprogramDes = macc.MaccPathParprogramDes
				});
			}

			return ResponseBase<List<MaccResponse>>.Success(res);
		}

		public async Task<ResponseBase<List<MaccResponse>>> GetAllAllowedMaccAsync()
		{
			var maccs = await _dbContext.FlussoMaccs.Where(x => x.MaccFlgAttivo == "S" && x.MaccCMatricola.Length == 2).ToListAsync();
			var res = new List<MaccResponse>();

			foreach (var macc in maccs)
			{
				res.Add(new MaccResponse
				{
					MaccCCdl = macc.MaccCCdl,
					MaccTipo = macc.MaccTipo,
					MaccCMatricola = macc.MaccCMatricola,
					MaccCDitta = macc.MaccCDitta,
					MaccDesc = macc.MaccDesc,
					MaccPassw = macc.MaccPassw,
					MaccFlgMag = macc.MaccFlgMag,
					MaccFlgGrp = macc.MaccFlgGrp,
					MaccNRis = macc.MaccNRis,
					MaccGrpCosto = macc.MaccGrpCosto,
					MaccDtIns = macc.MaccDtIns,
					MaccUtenteIns = macc.MaccUtenteIns,
					MaccDtUm = macc.MaccDtUm,
					MaccUtenteUm = macc.MaccUtenteUm,
					MaccPortata = macc.MaccPortata,
					MaccId = macc.MaccId,
					MaccStris = macc.MaccStris,
					MaccFlgAttivo = macc.MaccFlgAttivo,
					MaccFlgPresidio = macc.MaccFlgPresidio,
					MaccListaCaus = macc.MaccListaCaus,
					MaccTprocpers = macc.MaccTprocpers,
					MaccFlgComdir = macc.MaccFlgComdir,
					MaccPathParprogramSrc = macc.MaccPathParprogramSrc,
					MaccPathParprogramDes = macc.MaccPathParprogramDes
				});
			}

			return ResponseBase<List<MaccResponse>>.Success(res);
		}

		public async Task<ResponseBase<MaccResponse>> GetMaccByCdlAsync(string cdl)
		{
			var macc = await _dbContext.FlussoMaccs.FirstOrDefaultAsync(x => x.MaccCCdl == cdl);

			if (macc == null)
			{
				return ResponseBase<MaccResponse>.Failed(PscCo02Errors.RecordNonTrovati, "Non sono stati trovati record con il codice passato");
			}

			var res = new MaccResponse
			{
				MaccCCdl = macc.MaccCCdl,
				MaccTipo = macc.MaccTipo,
				MaccCMatricola = macc.MaccCMatricola,
				MaccCDitta = macc.MaccCDitta,
				MaccDesc = macc.MaccDesc,
				MaccPassw = macc.MaccPassw,
				MaccFlgMag = macc.MaccFlgMag,
				MaccFlgGrp = macc.MaccFlgGrp,
				MaccNRis = macc.MaccNRis,
				MaccGrpCosto = macc.MaccGrpCosto,
				MaccDtIns = macc.MaccDtIns,
				MaccUtenteIns = macc.MaccUtenteIns,
				MaccDtUm = macc.MaccDtUm,
				MaccUtenteUm = macc.MaccUtenteUm,
				MaccPortata = macc.MaccPortata,
				MaccId = macc.MaccId,
				MaccStris = macc.MaccStris,
				MaccFlgAttivo = macc.MaccFlgAttivo,
				MaccFlgPresidio = macc.MaccFlgPresidio,
				MaccListaCaus = macc.MaccListaCaus,
				MaccTprocpers = macc.MaccTprocpers,
				MaccFlgComdir = macc.MaccFlgComdir,
				MaccPathParprogramSrc = macc.MaccPathParprogramSrc,
				MaccPathParprogramDes = macc.MaccPathParprogramDes
			};

			return ResponseBase<MaccResponse>.Success(res);
		}

		public async Task<ResponseBase<MaccResponse>> GetMaccByRisFirstAsync(string risorsa)
		{
			var macc = await _dbContext.FlussoMaccs.FirstOrDefaultAsync(x => x.MaccCMatricola == risorsa);

			if (macc == null)
			{
				return ResponseBase<MaccResponse>.Failed(PscCo02Errors.RecordNonTrovati, "Non sono stati trovati record con il codice passato");
			}

			var res = new MaccResponse
			{
				MaccCCdl = macc.MaccCCdl,
				MaccTipo = macc.MaccTipo,
				MaccCMatricola = macc.MaccCMatricola,
				MaccCDitta = macc.MaccCDitta,
				MaccDesc = macc.MaccDesc,
				MaccPassw = macc.MaccPassw,
				MaccFlgMag = macc.MaccFlgMag,
				MaccFlgGrp = macc.MaccFlgGrp,
				MaccNRis = macc.MaccNRis,
				MaccGrpCosto = macc.MaccGrpCosto,
				MaccDtIns = macc.MaccDtIns,
				MaccUtenteIns = macc.MaccUtenteIns,
				MaccDtUm = macc.MaccDtUm,
				MaccUtenteUm = macc.MaccUtenteUm,
				MaccPortata = macc.MaccPortata,
				MaccId = macc.MaccId,
				MaccStris = macc.MaccStris,
				MaccFlgAttivo = macc.MaccFlgAttivo,
				MaccFlgPresidio = macc.MaccFlgPresidio,
				MaccListaCaus = macc.MaccListaCaus,
				MaccTprocpers = macc.MaccTprocpers,
				MaccFlgComdir = macc.MaccFlgComdir,
				MaccPathParprogramSrc = macc.MaccPathParprogramSrc,
				MaccPathParprogramDes = macc.MaccPathParprogramDes
			};

			return ResponseBase<MaccResponse>.Success(res);
		}
	}
}
