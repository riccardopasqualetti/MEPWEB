namespace MepWeb.DTO.Response
{
    public class CrrgErrorsResponse
    {
        public string Succeeded { get; set; } = "N";
        public List<Dictionary<string, string>> Errors { get; set; } = new List<Dictionary<string, string>>();
    }
}
