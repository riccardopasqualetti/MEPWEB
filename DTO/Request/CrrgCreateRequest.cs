using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mep01Web.DTO.Response;
using Mep01Web.Type.Dropdown;

namespace Mep01Web.DTO.Request
{
    public class CrrgCreateRequest // : IValidatableObject
    {
        public CrrgCreateRequest()
        {            
        }
        public CrrgCreateRequest(decimal crrgSCRL)
        {
            CrrgCSrl = crrgSCRL;
        }
        public CrrgCreateRequest(CrrgCCausList crrgCCausList)
        {
            CrrgCCausList = crrgCCausList;
        }

        // Id per Update e Delete
        public decimal CrrgCSrl { get; set; }

        // Risorsa
        [Required(ErrorMessage = "Risorsa obbligatoria")]
        [DisplayName("Risorsa")]
        public string CrrgCRis { get; set; }

        [Required(ErrorMessage = "CdL obbligatoria")]
        public string CrrgCdl { get; set; }

        // Data
        [Required(ErrorMessage = "Data obbligatoria")]
        public DateTime CrrgDtt { get; set; }
        // Tempo
        [Required(ErrorMessage = "Tempo obbligatorio")]
        public DateTime CrrgTmRunIncrHMS { get; set; }
        public DateTime CrrgTmRunIncrHMSProd { get; set; }

        // ISL
        [MaxLength(13)]
		//[Range(13, 13, ErrorMessage = "ISL deve essere di 13 caratteri")]
		public string? CrrgRifCliente { get; set; }


		// Codice cliente        		
		public string? ComCCli { get; set; }
        // Dropdown per ComCCli
        public AcliList AcliList { get; set; } = new AcliList();


		// Commessa		
		public string CrrgTstDoc { get; set; }        
        public string CrrgPrfDoc { get; set; }        
        public decimal CrrgADoc { get; set; }
        public decimal CrrgNDoc { get; set; }
        public decimal CrrgPosDoc { get; set; }
        public decimal CrrgPrgDoc { get; set; }
        // Codice commessa compatto
        public string CommCode { get; set; }
		// Dropdown per CommCode
		public TbcpList TbcpList { get; set; } = new TbcpList();


		//Operazione (NOper=0,50,ecc...) (TOper=M001,PS33,ecc...) 
		public decimal CrrgNOper { get; set; }
        public string CrrgTOper { get; set; }
        public string NTOper { get; set; }
		public NTOperList NTOperList { get; set; } = new NTOperList();


		//Macroattività (0=n.d.,1=Garanzia,2=Assistenza,3=Verbale) 
		public string CrrgCmaatt { get; set; }
        // Dropdown per CrrgCCaus
        public CrrgCmaattList CrrgCmaattList { get; set; } = new CrrgCmaattList();


		// Causale (CORI, ANFU, SVIL, DELI)
		public string CrrgCCaus { get; set; }		
		// Dropdown per CrrgCCaus
		public CrrgCCausList CrrgCCausList { get; set; } = new CrrgCCausList();

		
        // Descrizione
        public string? CrrgNote { get; set; }


        // Applicazione (APP_MEP,APP_MAKINGONE,APP_DEPSOL,ecc...))
        public string CrrgApp { get; set; }
		public AppList AppList { get; set; } = new AppList();

		// Modulo (MOD_COMDES,ecc...)
		public string CrrgMod { get; set; }
		public ModList ModList { get; set; } = new ModList();


        public string? MemoModalita { get; set; }

        public string Succeeded { get; set; } = "";
        public bool IsUpdate { get; set; } = false;

        /// <summary>
        /// [231009] True solo quando la modifica arriva dall'allineamento con Teamwork: è l'unico caso in cui
        /// un consuntivo collegato a una registrazione Teamwork può essere modificato o cancellato.
        /// Non viene mai letto dal form o dalle API.
        /// </summary>
        [BindNever]
        public bool DaAllineamentoTeamwork { get; set; } = false;

        /// <summary>
        /// [231009] Registrazione Teamwork a cui è collegato il consuntivo (flusso_crrg_tw), solo per le pagine di
        /// modifica e cancellazione: se valorizzato la pagina blocca risorsa, data, tempo e commessa/ISL.
        /// </summary>
        [BindNever]
        public long? TwTimelogId { get; set; }


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    throw new NotImplementedException();
        //}

        //public static implicit operator CrrgCreateRequest(Task<CrrgCreateRequest> v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
