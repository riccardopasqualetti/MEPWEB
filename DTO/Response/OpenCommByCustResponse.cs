namespace MepWeb.DTO.Response
{
	public class OpenCommByCustResponse
	{
		public string CommDescDd { get; set; } = null!;
		public string OrpbTstDoc { get; set; } = null!;
		public string OrpbPrfDoc { get; set; } = null!;
		public decimal OrpbADoc { get; set; }
		public decimal OrpbNDoc { get; set; }
	}
}
