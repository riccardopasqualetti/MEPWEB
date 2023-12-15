using System.ComponentModel.DataAnnotations;

namespace MepWeb.DTO.Request
{
    public class PscCo02UpdateRequest
    {
        public decimal IdDoc { get; set; }
        [Required(ErrorMessage = "CRisorsa obbligatoria")]
        public string CRisorsa { get; set; }

        public decimal Grpcdl { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
