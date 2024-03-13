using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Microsoft.EntityFrameworkCore;
using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.DTO;
using Newtonsoft.Json.Linq;
using Mep01Web.Service.Interface;
using MepWeb.DTO.Request;
using MepWeb.Costants;

namespace Mep01Web.Service.Impl
{
    public class TbcpService : ITbcpService
    {
        private readonly SataconsultingContext _db;

        public TbcpService(SataconsultingContext dbContext)
        {
            _db = dbContext;
        }


        public async Task<ResponseBase<TbcpResponse>?> GetTbcpByCodeAsync(TbcpGetRequest tbcpGetRequest)
        {
            FlussoTbcp? flussoTbcp = await _db.FlussoTbcps.SingleOrDefaultAsync(x => x.TbcpTstComm == tbcpGetRequest.CommTstDoc && x.TbcpPrfComm == tbcpGetRequest.CommPrfDoc && x.TbcpAComm == tbcpGetRequest.CommADoc && x.TbcpNComm == tbcpGetRequest.CommNDoc);
            if (flussoTbcp == null)
            {
                return ResponseBase<TbcpResponse?>.Failed("-1", $"Non sono stati trovati record con questo valore '{tbcpGetRequest.CommTstDoc}/{tbcpGetRequest.CommPrfDoc}/{tbcpGetRequest.CommADoc}/{tbcpGetRequest.CommNDoc}'");
            }

            var tbcpResponse = new TbcpResponse();

            tbcpResponse.CommMasterData = flussoTbcp;
            //acli_rag_soc_1 = from flusso_tbcp left outer join flusso_acli on acli_c_cliente = flusso_tbcp.tbcp_c_cli
            if (flussoTbcp.TbcpCCli != null)
            {
                FlussoAcli? flussoAcli = await _db.FlussoAclis.SingleOrDefaultAsync(x => x.AcliCCliente == flussoTbcp.TbcpCCli);
                tbcpResponse.CommRCli = flussoAcli.AcliRagSoc1;
            }

            return ResponseBase<TbcpResponse?>.Success(tbcpResponse);
        }


        public async Task<ResponseBase<TbcpResponse>?> GetTbcpByCompCodeAsync(TbcpGetRequest tbcpGetRequest)
        {
            var commTstDoc = tbcpGetRequest.CommCompCode.Substring(0, 3);
            var commPrfDoc = tbcpGetRequest.CommCompCode.Substring(3, 1);
            var commADoc = Decimal.Parse(tbcpGetRequest.CommCompCode.Substring(4, 4));
            var commNDoc = Decimal.Parse(tbcpGetRequest.CommCompCode.Substring(8, 6));

            FlussoTbcp? flussoTbcp = await _db.FlussoTbcps.SingleOrDefaultAsync(x => x.TbcpTstComm == commTstDoc && x.TbcpPrfComm == commPrfDoc && x.TbcpAComm == commADoc && x.TbcpNComm == commNDoc);
            if (flussoTbcp == null)
            {
                return ResponseBase<TbcpResponse?>.Failed("-2", $"Non sono stati trovati record con questo valore '{tbcpGetRequest}'");
            }

            var tbcpResponse = new TbcpResponse();

            tbcpResponse.CommMasterData = flussoTbcp;
            //acli_rag_soc_1 = from flusso_tbcp left outer join flusso_acli on acli_c_cliente = flusso_tbcp.tbcp_c_cli
            if (flussoTbcp.TbcpCCli != null)
            {
                FlussoAcli? flussoAcli = await _db.FlussoAclis.SingleOrDefaultAsync(x => x.AcliCCliente == flussoTbcp.TbcpCCli);
                tbcpResponse.CommRCli = flussoAcli.AcliRagSoc1;
            }

            return ResponseBase<TbcpResponse?>.Success(tbcpResponse);
        }

        public async Task<ResponseBase<List<TbcpResponse>>?> GetTbcpAllByCliAsync(TbcpGetRequest tbcpGetRequest)
        {
            List<FlussoTbcp> flussoTbcpList;
            if (tbcpGetRequest.CommCCli == null)
            {
                flussoTbcpList = await _db.FlussoTbcps.ToListAsync();
            }
            else
            {
                flussoTbcpList = await _db.FlussoTbcps.Where(x => x.TbcpCCli == tbcpGetRequest.CommCCli).ToListAsync();
            }

            if (flussoTbcpList == null)
            {
                return ResponseBase<List<TbcpResponse>?>.Failed("-4", $"Non sono stati trovati record in flusso_acli");
            }

            List<TbcpResponse> tbcpResponseList = new List<TbcpResponse>();
            foreach (var flussoTbcp in flussoTbcpList)
            {
                var tbcpResponse = new TbcpResponse();
                tbcpResponse.CommMasterData = flussoTbcp;

                if (flussoTbcp.TbcpCCli != null)
                {
                    FlussoAcli? flussoAcli = await _db.FlussoAclis.SingleOrDefaultAsync(x => x.AcliCCliente == flussoTbcp.TbcpCCli);
                    if (flussoAcli != null)
                    {
                        tbcpResponse.CommRCli = flussoAcli.AcliRagSoc1;
                    }
                }

                tbcpResponseList.Add(tbcpResponse);
            }

            return ResponseBase<List<TbcpResponse>?>.Success(tbcpResponseList);
        }

        public async Task<ResponseBase<List<TbcpLightResponse>>?> GetTbcpAllLightByCliAsync(TbcpGetRequest tbcpGetRequest)
        {
            List<FlussoTbcp> flussoTbcpList;
            if (tbcpGetRequest.CommCCli == null)
            {
                flussoTbcpList = await _db.FlussoTbcps.ToListAsync();
            }
            else
            {
                flussoTbcpList = await _db.FlussoTbcps.Where(x => x.TbcpCCli == tbcpGetRequest.CommCCli).ToListAsync();
            }

            if (flussoTbcpList == null)
            {
                return ResponseBase<List<TbcpLightResponse>?>.Failed("-4", $"Non sono stati trovati record in flusso_acli");
            }

            List<TbcpLightResponse> tbcpResponseList = new List<TbcpLightResponse>();
            foreach (var flussoTbcp in flussoTbcpList)
            {
                var tbcpResponse = new TbcpLightResponse();
                tbcpResponse.CommCompCode = flussoTbcp.TbcpTstComm + "/" + flussoTbcp.TbcpPrfComm + "/" + flussoTbcp.TbcpAComm + "/" + flussoTbcp.TbcpNComm.ToString("000000");
                tbcpResponse.CommDesc = flussoTbcp.TbcpDesc;
                tbcpResponse.CommCCli = flussoTbcp.TbcpCCli;

                if (flussoTbcp.TbcpCCli != null)
                {
                    FlussoAcli? flussoAcli = await _db.FlussoAclis.SingleOrDefaultAsync(x => x.AcliCCliente == flussoTbcp.TbcpCCli);
                    if (flussoAcli != null)
                    {
                        tbcpResponse.CommRCli = flussoAcli.AcliRagSoc1;
                    }
                }

                tbcpResponseList.Add(tbcpResponse);
            }

            return ResponseBase<List<TbcpLightResponse>?>.Success(tbcpResponseList);
        }

        public async Task<ResponseBase<TbcpLightResponse>> GetTbcpLightByNumAsync(TbcpGetRequest tbcpGetRequest)
        {

            FlussoTbcp flussoTbcp = await _db.FlussoTbcps.SingleOrDefaultAsync(x => x.TbcpNComm == tbcpGetRequest.CommNDoc);

            if (flussoTbcp == null)
            {
                return ResponseBase<TbcpLightResponse?>.Failed("-5", $"Record commessa non trovato in flusso_tbcp");
            }

            var tbcpResponse = new TbcpLightResponse();
            tbcpResponse.CommCompCode = flussoTbcp.TbcpTstComm + "/" + flussoTbcp.TbcpPrfComm + "/" + flussoTbcp.TbcpAComm + "/" + flussoTbcp.TbcpNComm.ToString("000000");
            tbcpResponse.CommDesc = flussoTbcp.TbcpDesc;
            tbcpResponse.CommCCli = flussoTbcp.TbcpCCli;

            return ResponseBase<TbcpLightResponse?>.Success(tbcpResponse);

        }

        public async Task<ResponseBase<TbcpUpdateCampiOpzionaliRequest>> UpdateCampiOpzionaliAsync(TbcpUpdateCampiOpzionaliRequest request)
        {
            var tbcpRiga = await _db.FlussoTbcps.FirstOrDefaultAsync(x =>
            x.TbcpTstComm == request.TstComm &&
            x.TbcpPrfComm == request.PrfComm &&
            x.TbcpAComm == request.AComm &&
            x.TbcpNComm == request.NComm);

            if (tbcpRiga == null)
            {
                return ResponseBase<TbcpUpdateCampiOpzionaliRequest>.Failed(CommonCostants.RecordNonTrovato, "Non è stato trovato il record con il documento selezionato");
            }

            if (!string.IsNullOrWhiteSpace(request.Nota))
            {
                tbcpRiga.TbcpFfllT1 = request.Nota;
            }

            if (!string.IsNullOrWhiteSpace(request.Preoccupazione))
            {
                tbcpRiga.TbcpFfllA1 = request.Preoccupazione;
            }

            if (request.Avanzamento != null && (request.Avanzamento > 100 || request.Avanzamento < 0))
            {
                return ResponseBase<TbcpUpdateCampiOpzionaliRequest>.Failed(CommonCostants.ValoreNonValido, "Il valore di 'Avanzamento' non è valido. deve essere un valore tra 0 e 100");
            }

            if (request.Avanzamento != null)
            {
                tbcpRiga.TbcpFfllN1 = request.Avanzamento.Value;
            }

            try
            {
                var righe = await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ResponseBase<TbcpUpdateCampiOpzionaliRequest>.Success();

        }
    }
}
