namespace MepWeb.DTO.Request
{
    public class BasePagedRequest
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
    }
}
