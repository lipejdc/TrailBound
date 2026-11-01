namespace TrailBound.Application.Dtos.Pagination;

public abstract class PaginationFilter<T> where T : class
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
    public int GetSkipCount() => (PageNumber - 1) * PageSize;
}
