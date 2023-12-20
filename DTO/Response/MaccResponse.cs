namespace MepWeb.DTO.Response
{
	public class MaccResponse
	{
		public string? MaccCCdl { get; set; }
		public string MaccTipo { get; set; } = null!;
		public string MaccCMatricola { get; set; } = null!;
		public string MaccCDitta { get; set; } = null!;
		public string? MaccDesc { get; set; }
		public string? MaccPassw { get; set; }
		public string? MaccFlgMag { get; set; }
		public string? MaccFlgGrp { get; set; }
		public decimal MaccNRis { get; set; }
		public string? MaccGrpCosto { get; set; }
		public DateTime? MaccDtIns { get; set; }
		public string? MaccUtenteIns { get; set; }
		public DateTime? MaccDtUm { get; set; }
		public string? MaccUtenteUm { get; set; }
		public decimal MaccPortata { get; set; }
		public decimal MaccId { get; set; }
		public decimal MaccStris { get; set; }
		public string? MaccFlgAttivo { get; set; }
		public string? MaccFlgPresidio { get; set; }
		public string? MaccListaCaus { get; set; }
		public decimal MaccTprocpers { get; set; }
		public string? MaccFlgComdir { get; set; }
		public string? MaccPathParprogramSrc { get; set; }
		public string? MaccPathParprogramDes { get; set; }
	}
}
