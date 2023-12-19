namespace MepWeb.DTO.Response
{
	public class TcdlResponse
	{
		public string TcdlCCdl { get; set; } = null!;
		public string TcdlCDitta { get; set; } = null!;
		public string? TcdlDesc { get; set; }
		public string? TcdlFlgSched { get; set; }
		public string? TcdlCCale { get; set; }
		public string? TcdlTDomina { get; set; }
		public string? TcdlFlgIe { get; set; }
		public string? TcdlCCdc { get; set; }
		public string? TcdlFlgDett { get; set; }
		public decimal TcdlNEntProd { get; set; }
		public decimal TcdlHhPrevMese { get; set; }
		public decimal TcdlHhMedMese { get; set; }
		public decimal TcdlHhTurno { get; set; }
		public decimal TcdlPercEff { get; set; }
		public decimal TcdlTmAggSec { get; set; }
		public decimal TcdlColor { get; set; }
		public DateTime? TcdlDtIns { get; set; }
		public string? TcdlUtenteIns { get; set; }
		public DateTime? TcdlDtUm { get; set; }
		public string? TcdlUtenteUm { get; set; }
		public string? TcdlTProp { get; set; }
		public string? TcdlCProp { get; set; }
		public string? TcdlFlgSchedMano { get; set; }
		public string? TcdlCGruppoSched { get; set; }
		public decimal TcdlCalgassris { get; set; }
		public decimal TcdlCalgschris { get; set; }
		public DateTime? TcdlDtUltimaSched { get; set; }
		public decimal TcdlGrpcdl { get; set; }
		public string? TcdlCLivP { get; set; }
		public decimal TcdlId { get; set; }
		public decimal TcdlCMagVers { get; set; }
		public decimal TcdlCMagPrel { get; set; }
	}
}
