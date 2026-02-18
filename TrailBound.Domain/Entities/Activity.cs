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
    public DateTime Date { get; set; }
    public TimeSpan Duration { get; set; }
    public double DistanceInKm { get; set; }
    public int ElevationGain { get; set; }
    public int ElevationLoss { get; set; }
    public string? GpxFilePath { get; set; }
    public string? KomootUrl { get; set; }
    public ActivityStatus Status { get; set; }

    public int? TripId { get; set; } //Optional foreign key
    public Trip? Trip { get; set; } //Navigation property

    //Method to assign a trip
    public void AssignActivityToTrip(Trip trip)
    {
        ArgumentNullException.ThrowIfNull(trip);

        Trip = trip;
        TripId = trip.Id;
    }
}
