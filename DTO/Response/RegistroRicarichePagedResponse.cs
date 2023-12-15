namespace MepWeb.DTO.Response
{
	public class RegistroRicarichePagedResponse
	{
		public int TotalPages { get; set; }
		public int TotalRecords { get; set; }
		public List<RegistroRicaricheResponse> response { get; set; }
	}
}
