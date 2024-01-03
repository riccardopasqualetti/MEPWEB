namespace MepWeb.DTO.Response
{
    public class PscCo02Response
    {
        public string CDitta { get; set; } 

        public decimal IdDocumento { get; set; }

        public string CRisorsa { get; set; }
		public decimal Qualifica { get; set; }
		public string DescrizioneQualifica { get; set; }

		public string? UtenteIns { get; set; }

        public DateTime? DtIns { get; set; }

        public string? UtenteUm { get; set; }

        public DateTime? DtUm { get; set; }
    }
}
