namespace MepWeb.DTO.Response
{
	public class PscQualResponse
	{
		public string TSoggetto { get; set; } = null!;
		public string CSoggetto { get; set; } = null!;
		public decimal TindProgressivo { get; set; }
		public string CRisorsa { get; set; } = null!;
		public decimal? Grpcdl { get; set; }
		public string CDitta { get; set; } = null!;
		public string? UtenteIns { get; set; }
		public DateTime? DtIns { get; set; }
		public string? UtenteUm { get; set; }
		public DateTime? DtUm { get; set; }
	}
}
