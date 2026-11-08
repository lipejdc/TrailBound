namespace TrailBound.Application.Dtos.Pagination;

using Microsoft.AspNetCore.Mvc;
using TrailBound.Domain.Entities;

public class ActivityPaginationFilter : PaginationFilter<Activity>
{
    public string? ActivityType { get; set; } = string.Empty;

    public string? Title { get; set; } = string.Empty;

    public DateTimeOffset? StartDate { get; set; } = DateTimeOffset.MinValue;
    public DateTimeOffset? EndDate { get; set; } = DateTimeOffset.MaxValue;

    public TimeSpan? MinimalDuration { get; set; }

    public TimeSpan? MaximalDuration { get; set; }

    public int? MinimalDistanceInMeters { get; set; } = 0;
    public int? MaximalDistanceInMeters { get; set; } = int.MaxValue;
}
