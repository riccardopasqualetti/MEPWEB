namespace MepWeb.DTO.Response
{
	public class PscCo02PagedResponse
	{
		public int TotalPages { get; set; }
		public int TotalRecords { get; set; }
		public List<PscCo02Response?> response { get; set; }
	}
}
