namespace MepWeb.DTO
{
    public class BasePageCriteria
    {
        public BasePageCriteria(int page, int limit)
        {
            Page = Math.Max(1, page);
            Limit = Math.Max(1, limit);

            Offset = (Page - 1) * Limit;
        }
        public int Page { get; }
        public int Limit { get; }
        public int Offset { get; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
    }
}
