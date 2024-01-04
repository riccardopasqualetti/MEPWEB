using System.ComponentModel.DataAnnotations;

namespace MepWeb.DTO.Request
{
    public class PscCo02UpdateRequest
    {
		[Required(ErrorMessage = "Id Documento obbligatorio")]
		public decimal IdDocumento { get; set; }
		[Required(ErrorMessage = "Codice Risorsa obbligatorio")]
		public string CRisorsa { get; set; }
		//public string DescrizioneQualifica { get; set; }
		public decimal? Qualifica { get; set; }
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
