using System.ComponentModel.DataAnnotations;

namespace MepWeb.DTO.Request
{
    
    public class PscCo02CreateRequest : IValidatableObject
    {
       
        public string CDitta { get; set; } 

        public decimal IdDoc { get; set; }
        [Required(ErrorMessage = "CRisorsa obbligatoria")]
        public string CRisorsa { get; set; }

        public decimal Grpcdl { get; set; }

        public string? UtenteIns { get; set; }

        public DateTime? DtIns { get; set; }

        public string? UtenteUm { get; set; }

        public DateTime? DtUm { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
