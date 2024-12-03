namespace Cinema.Domain.Core.Pagination;

public class PaginationResponse<T>
{
    public IEnumerable<T> Data { get; set; } = [];
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public SortingOrder SortOrder { get; set; }
    public string SortBy { get; set; }
}