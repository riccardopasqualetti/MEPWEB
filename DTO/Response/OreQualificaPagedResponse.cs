namespace MepWeb.DTO.Response
{
    public class OreQualificaPagedResponse
    {
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public List<OreQualificaResponse> response { get; set; }
    }
}
