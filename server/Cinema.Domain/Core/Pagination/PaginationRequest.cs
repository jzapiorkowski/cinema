using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Domain.Core.Pagination;

public class PaginationRequest
{
    [Range(1, int.MaxValue)]
    [DefaultValue(1)]
    public int PageNumber { get; set; } = 1;
    [Range(1, int.MaxValue)]
    [DefaultValue(10)]
    public int PageSize { get; set; } = 10;
    [DefaultValue(SortingOrder.Asc)]
    public SortingOrder SortOrder { get; set; } = SortingOrder.Asc;
    public string SortBy { get; set; } = "Id";
}