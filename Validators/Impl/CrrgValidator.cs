using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Mep01Web.Exeptions;
using Mep01Web.Infrastructure;
using Mep01Web.Models;
using Mep01Web.DTO.Request;
using Mep01Web.DTO.Response;
using Mep01Web.DTO;
using MepWeb.Costants;

namespace Mep01Web.Validators.Impl
{
    public class CrrgValidator : ICrrgValidator
    {
        private readonly SataconsultingContext _dbContext;

        public CrrgValidator(SataconsultingContext context)
        {
            _dbContext = context;
        }
        public async Task<ResponseBase<CrrgResponse?>> CrrgValidateAsync(CrrgCreateRequest crrgRequest)
        {

            if (string.IsNullOrWhiteSpace(crrgRequest.CrrgCRis))
            {
				//throw new CrrgException(-1, $"Risorsa non indicata (s)");
				return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CrrgCRis, $"Risorsa non indicata (s)");
			}

            // test provvisorio
            DateTime dd1 = new(2023, 01, 01, 0, 0, 0);
            DateTime dd2 = DateTime.Now.AddDays(3);

			if (string.IsNullOrWhiteSpace(crrgRequest.CrrgCdl))
			{
				crrgRequest.CrrgCdl = (await _dbContext.FlussoMaccs.FirstOrDefaultAsync(x => x.MaccCMatricola == crrgRequest.CrrgCRis)).MaccCCdl;
			}

            if (crrgRequest.CrrgDtt < dd1 || crrgRequest.CrrgDtt > dd2)
            {   
				return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CrrgDtt, $"Data non valida (s)");
			}
            if (crrgRequest.CrrgTmRunIncrHMS.Hour == 0 && crrgRequest.CrrgTmRunIncrHMS.Minute == 0 && crrgRequest.CrrgTmRunIncrHMS.Second == 0)
            {                
				return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CrrgTmRunIncrHMS, $"Durata non valida (s)");
			}


			// Validazione ISL - Valorizzazione di default Commessa
			// Se la ISL è valorizzata, la Commessa, l'Applicativo e il Modulo sono presi di default dalla ISL.
			string? flgcom = null;
            if (!string.IsNullOrWhiteSpace(crrgRequest.CrrgRifCliente))
            {
                var ISL = await _dbContext.FlussoTatvs.SingleOrDefaultAsync(x => x.TatvRifCliente == crrgRequest.CrrgRifCliente);
                if (ISL == null)
                {                    
					return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CrrgRifCliente, $"ISL inesistente (s)");
				}
                crrgRequest.CrrgTstDoc = ISL.TatvTstComm;
                crrgRequest.CrrgPrfDoc = ISL.TatvPrfComm;
                crrgRequest.CrrgADoc = ISL.TatvAComm;
				crrgRequest.CrrgNDoc = ISL.TatvNComm;
				crrgRequest.CrrgApp = ISL.TatvCPartApp;
				crrgRequest.CrrgMod = ISL.TatvCPart;
				flgcom = "-5";

				if (crrgRequest.CrrgCCaus == "CORI")
				{
					return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CrrgCCaus, $"Causale non ammessa");
				}

				if (ISL.TatvFlgOfferta > 0 && crrgRequest.CrrgCmaatt != "3")
				{
					return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CrrgCmaatt, $"Verbale obbligatorio per ISL con offerta");
				}
			}
			// Se la ISL non è valorizzata, ma è valorizzato il campo CommCode con il codice in formato compatto,
            // la commessa è presa da quest'ultimo campo.
			// Altrimenti, mantiene i campi della form inalterati.
			else
			{
                try {
					
					if (!string.IsNullOrWhiteSpace(crrgRequest.CommCode))
					{
						crrgRequest.CrrgTstDoc = crrgRequest.CommCode.Substring(0, 3);
						crrgRequest.CrrgPrfDoc = crrgRequest.CommCode.Substring(4, 1);
						crrgRequest.CrrgADoc = Decimal.Parse(crrgRequest.CommCode.Substring(6, 4));
						crrgRequest.CrrgNDoc = Decimal.Parse(crrgRequest.CommCode[11..]);
						flgcom = "-7";
					}

					if (crrgRequest.CrrgCCaus != "CORI")
					{
						return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CrrgCCaus, $"Causale non ammessa");
					}

				}
				catch
                {
					return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CommCode, $"Commessa non valida: {crrgRequest.CommCode ?? ""}");
				}
				if (crrgRequest.CrrgCCaus != "CORI")
				{
					return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CrrgCCaus, $"Causale non ammessa");
				}
			}


			// Validazione Commessa
			if (crrgRequest.CrrgPosDoc == 0)
            {
                crrgRequest.CrrgPosDoc = 1;
			}
			if (crrgRequest.CrrgPrgDoc == 0)
			{
                crrgRequest.CrrgPrgDoc = 1;
			}
			var commessa = await _dbContext.FlussoTbcps.SingleOrDefaultAsync(x => x.TbcpTstComm == crrgRequest.CrrgTstDoc && x.TbcpPrfComm == crrgRequest.CrrgPrfDoc && x.TbcpAComm == crrgRequest.CrrgADoc && x.TbcpNComm == crrgRequest.CrrgNDoc);
			if (commessa == null)
			{				
				return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CommCode, $"Commessa inesistente");
			}

			var testCommessa = await _dbContext.VsPpCommAperteXClis.FirstOrDefaultAsync(x => x.OrpbTstDoc == crrgRequest.CrrgTstDoc && x.OrpbPrfDoc == crrgRequest.CrrgPrfDoc && x.OrpbADoc == crrgRequest.CrrgADoc && x.OrpbNDoc == crrgRequest.CrrgNDoc);

			if (testCommessa == null)
			{
				return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CommCode, "Commessa chiusa");
			}

			// Validazione Numero Operazione e Tipo Operazione 
			// Se il campo NTOper è valorizzato, i campi CrrgNOper e CrrgTOper vengono valorizzati con i valori 
			// del primo che devono essere formattati separati da '-'.
			// Altrimenti, mantiene i campi della form inalterati.
			if (!string.IsNullOrWhiteSpace(crrgRequest.NTOper))
			{
				try
				{
					string[] subs = crrgRequest.NTOper.Split('-');
					crrgRequest.CrrgNOper = Decimal.Parse(subs[0]);
					crrgRequest.CrrgTOper = subs[1];
				}
				catch
				{
				}
			}
			FlussoOlca flussoOlca = await _dbContext.FlussoOlcas.SingleOrDefaultAsync(x => x.OlcaTstDoc == crrgRequest.CrrgTstDoc && x.OlcaPrfDoc == crrgRequest.CrrgPrfDoc && x.OlcaADoc == crrgRequest.CrrgADoc && x.OlcaNDoc == crrgRequest.CrrgNDoc && x.OlcaNOper == crrgRequest.CrrgNOper && x.OlcaTOper == crrgRequest.CrrgTOper);
			if (flussoOlca == null)
			{
				return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.NTOper, $"Operazione inesistente (s)");
			}


			// Validazione Applicativo			
			FlussoTbpn flussoTbpn;
			if (!string.IsNullOrWhiteSpace(crrgRequest.CrrgApp))
			{				
				flussoTbpn = await _dbContext.FlussoTbpns.SingleOrDefaultAsync(x => x.TbpnCPart == crrgRequest.CrrgApp && crrgRequest.CrrgApp.StartsWith("APP_"));
				if (flussoTbpn == null && !string.IsNullOrWhiteSpace(crrgRequest.CrrgApp))
				{
					return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CrrgApp, $"Applicativo inesistente (s)");
				}
			}

			// Validazione Modulo			
			if (!string.IsNullOrWhiteSpace(crrgRequest.CrrgMod))
			{
				flussoTbpn = await _dbContext.FlussoTbpns.SingleOrDefaultAsync(x => x.TbpnCPart == crrgRequest.CrrgMod && crrgRequest.CrrgMod.StartsWith("MOD_"));
				if (flussoTbpn == null && !string.IsNullOrWhiteSpace(crrgRequest.CrrgMod))
				{
					return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CrrgMod, $"Modulo inesistente (s)");
				}
			}

			//controllo che il campo note sia valorizzato
			if (string.IsNullOrWhiteSpace(crrgRequest.CrrgNote))
			{
				return ResponseBase<CrrgResponse?>.Failed(CrrgCreateErrors.CrrgNote, $"Campo note obligatorio");
			}

            return ResponseBase<CrrgResponse?>.Success();
		}
	}
}
