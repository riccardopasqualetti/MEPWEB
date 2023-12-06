using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Microsoft.EntityFrameworkCore;
using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.DTO;
using Mep01Web.Service.Interface;

namespace Mep01Web.Service.Impl
{
    public class TatvService : ITatvService
    {
        private readonly SataconsultingContext _db;
        private readonly ITbcpService _tbcpService;

        public TatvService(SataconsultingContext dbContext, ITbcpService tbcpService)
        {
            _db = dbContext;
            _tbcpService = tbcpService;
        }

        public async Task<ResponseBase<TatvResponse>?> GetTatvAsync(TatvGetRequest tatvGetRequest)
        {
            FlussoTatv? flussoTatv = await _db.FlussoTatvs.SingleOrDefaultAsync(x => x.TatvRifCliente == tatvGetRequest.codeISl);
			if (flussoTatv == null)
			{
				return ResponseBase<TatvResponse?>.Failed("-1", $"Non sono stati trovati record con questo valore '{tatvGetRequest.codeISl}'");
			}

			var tatvResponse = new TatvResponse();


			tatvResponse.ISLMasterData = flussoTatv; //ISLMasterData è di tipo FlussoTatv

			//acli_rag_soc_1 = from flusso_tatv left outer join flusso_acli on acli_c_cliente = flusso_tatv.tatv_c_cli
			FlussoAcli? flussoAcli = await _db.FlussoAclis.SingleOrDefaultAsync(x => x.AcliCCliente == flussoTatv.TatvCCli);

            if (flussoAcli == null)  
			{
				return ResponseBase<TatvResponse?>.Failed("-2", $"Non è stato trovato il cliente in flusso_acli per '{tatvGetRequest.codeISl}'");
			}                
                
            tatvResponse.ISLRCli = flussoAcli.AcliRagSoc1;

            //CommData? comm = await GetCommByCodeAsync(isl.TatvTstComm, isl.TatvPrfComm, isl.TatvAComm, isl.TatvNComm);
                
            var tbcpGetRequest = new TbcpGetRequest() { 
                CommTstDoc = tatvResponse.ISLMasterData.TatvTstComm,
                CommPrfDoc = tatvResponse.ISLMasterData.TatvPrfComm,
                CommADoc = tatvResponse.ISLMasterData.TatvAComm, 
                CommNDoc = tatvResponse.ISLMasterData.TatvNComm
            };
            //TbcpGetResponse
            //TbcpGetResponse
            var tbcpGetResponse = await _tbcpService.GetTbcpByCodeAsync(tbcpGetRequest);
			tatvResponse.ISLCommData = tbcpGetResponse.Body;
                       
			return ResponseBase<TatvResponse?>.Success(tatvResponse);

		}
    }
}
