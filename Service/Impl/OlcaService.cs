using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.DTO;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Microsoft.EntityFrameworkCore;
using Mep01Web.Service.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Mep01Web.Service.Impl
{
    public class OlcaService : IOlcaService
	{
		//select olca_liv_oper, olca_t_oper, olca_n_oper, olca_n_fraz_doc, cito_descrizione, * from flusso_olca, flusso_cito
		//where
		//olca_tst_doc = 'COV'
		//and olca_prf_doc = 'A'
		//and olca_a_doc = 2023
		//and olca_n_doc = 231006
		//and olca_n_riga_doc = 1
		//and cito_codice = olca_t_oper
		//and cito_c_ditta = olca_c_ditta


		private readonly SataconsultingContext _db;

		public OlcaService(SataconsultingContext dbContext)
		{
			_db = dbContext;
		}

		public async Task<ResponseBase<OlcaResponse>?> GetOlcaAsync(OlcaGetRequest olcaGetRequest)
		{
			FlussoOlca flussoOlca = await _db.FlussoOlcas.SingleOrDefaultAsync(x => x.OlcaTstDoc == olcaGetRequest.OlcaTstDoc && x.OlcaPrfDoc == olcaGetRequest.OlcaPrfDoc && x.OlcaADoc == olcaGetRequest.OlcaADoc && x.OlcaNDoc == olcaGetRequest.OlcaNDoc && x.OlcaNRigaDoc == olcaGetRequest.OlcaNRigaDoc && x.OlcaNFrazDoc == olcaGetRequest.OlcaNFrazDoc && x.OlcaNOper == olcaGetRequest.OlcaNOper);

			if (flussoOlca == null)
			{
				return ResponseBase<OlcaResponse?>.Failed("-1", $"Non sono stati trovati record con questo valore '{olcaGetRequest.OlcaTstDoc}/{olcaGetRequest.OlcaPrfDoc}/{olcaGetRequest.OlcaADoc}/{olcaGetRequest.OlcaNDoc}/{olcaGetRequest.OlcaNRigaDoc}'");
			}
			var olcaResponse = new OlcaResponse();
			olcaResponse.FlussoOlca = flussoOlca;
			return ResponseBase<OlcaResponse?>.Success(olcaResponse);
		}

		public async Task<ResponseBase<OlcaCitoResponse>?> GetOlcaCitoAsync(OlcaGetRequest olcaGetRequest)
		{
			List<FlussoOlca> flussoOlcaList = await _db.FlussoOlcas.Where(x => x.OlcaTstDoc == olcaGetRequest.OlcaTstDoc && x.OlcaPrfDoc == olcaGetRequest.OlcaPrfDoc && x.OlcaADoc == olcaGetRequest.OlcaADoc && x.OlcaNDoc == olcaGetRequest.OlcaNDoc && x.OlcaNRigaDoc == olcaGetRequest.OlcaNRigaDoc).ToListAsync(); 

			if (flussoOlcaList == null)
			{
				return ResponseBase<OlcaCitoResponse?>.Failed("-1", $"Non sono stati trovati record in flusso_olca");
			}

			OlcaCitoResponse olcaCitoResponse = new OlcaCitoResponse();

			List<OlcaCito> olcaCitoList = new List<OlcaCito>();

			foreach (FlussoOlca flussoOlca in flussoOlcaList)
			{
				var flussoCito = await _db.FlussoCitos.SingleOrDefaultAsync(x => x.CitoCDitta == flussoOlca.OlcaCDitta && x.CitoCodice == flussoOlca.OlcaTOper);

				if (flussoCito == null)
				{
					return ResponseBase<OlcaCitoResponse?>.Failed("-1", $"Non sono stati trovati record in flusso_cito");
				}
				OlcaCito el = new OlcaCito(flussoOlca, flussoCito);
				olcaCitoList.Add(el);
			}
			olcaCitoResponse.OlcaCitoList = olcaCitoList;

			return ResponseBase<OlcaCitoResponse?>.Success(olcaCitoResponse);
		}
	}
}
