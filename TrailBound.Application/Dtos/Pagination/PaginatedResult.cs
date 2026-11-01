namespace TrailBound.Application.Dtos.Pagination;

public class PaginatedResult<T> where T : class
{
    public List<T> EntityPaginatedResult { get; set; } = new List<T>();

    public int CurrentPage { get; }
    public int TotalPages { get; }
    public int TotalItems { get; }
    public int ItemsPerPage { get; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PaginatedResult(
        List<T> entityPaginatedResult, 
        int currentPage,
        int itemsPerPage,
        int totalItems)
    {
        EntityPaginatedResult = entityPaginatedResult;
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;

        if (itemsPerPage > 0 && totalItems > 0)
        {
            TotalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
        }
    }
}
