using TrailBound.Domain.Enums;

namespace TrailBound.Domain.Entities;

/// <summary>
/// Represents a Trip, which can have zero or more Activities.
/// </summary>

public class Trip
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public TripCategory Categories { get; set; }
    public Location Location { get; set; } = null!;
    public string? GoogleMapsUrl { get; set; }
}
