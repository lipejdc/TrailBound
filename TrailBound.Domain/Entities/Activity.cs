using TrailBound.Domain.Enums;

namespace TrailBound.Domain.Entities;

/// <summary>
/// Represents an individual activity, such as hiking, cycling or mountaineering.
/// An Activity can optionally belong to a trip.
/// </summary>

public class Activity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public ActivityType Type { get; set; }
    public Location Location { get; set; } = null!;
    public DateTimeOffset Date { get; set; }
    public TimeSpan Duration { get; set; }
    public Route Route { get; set; } = null!;
    public string? GpxFilePath { get; set; }
    public string? KomootUrl { get; set; }
    public ActivityStatus Status { get; set; }
    public int? TripId { get; set; } //Optional foreign key
    public Trip? Trip { get; set; } //Navigation property
}
