using TrailBound.Domain.Enums;

namespace TrailBound.Domain.Entities;

/// <summary>
/// Represents an individual activity, such as hiking, cycling or mountaineering.
/// An Activity can optionally belong to a trip.
/// </summary>

public class Activity
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public ActivityType Type { get; private set; }
    public Location Location { get; private set; }
    public DateTime Date { get; private set; }
    public TimeSpan Duration { get; private set; }
    public double DistanceInKm { get; private set; }
    public int ElevationGain { get; private set; }
    public int ElevationLoss { get; private set; }
    public string? GpxFilePath { get; private set; }
    public string? KomootUrl { get; private set; }
    public ActivityStatus Status { get; private set; }

    public int? TripId { get; private set; } //Optional foreign key
    public Trip? Trip { get; private set; } //Navigation property

    private Activity() //Private constructor for EF Core
    { 
        Title = null!; 
        Location = null!; 
    } 
    
    public Activity
        (
        string title, 
        ActivityType type, 
        Location location, 
        DateTime date, 
        double distanceInKm,
        ActivityStatus status
        )
    {

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty!", nameof(title));
        }

        ArgumentNullException.ThrowIfNull(location);

        if (distanceInKm < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(distanceInKm), "Distance cannot be negative!");
        }

        Title = title;
        Type = type;
        Location = location;
        Date = date;
        DistanceInKm = distanceInKm;
        Status = status;
    }

    //Method to assign a trip
    public void AssignActivityToTrip(Trip trip)
    {
        ArgumentNullException.ThrowIfNull(trip);

        Trip = trip;
        TripId = trip.Id;
    }
}
