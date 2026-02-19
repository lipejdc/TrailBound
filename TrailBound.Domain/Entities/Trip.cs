using TrailBound.Domain.Enums;

namespace TrailBound.Domain.Entities;

/// <summary>
/// Represents a Trip, which can have zero or more Activities.
/// </summary>

public class Trip
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public TripCategory Categories { get; set; }
    public string? GoogleMapsUrl { get; set; }

    //Activities belonging to this trip
    private readonly List<Activity> _activities = new();
    public IReadOnlyList<Activity> Activities => _activities.AsReadOnly();

    public void AddActivityToTrip(Activity activity)
    {
        ArgumentNullException.ThrowIfNull(activity);
        if (_activities.Contains(activity)) return;

        _activities.Add(activity);
        activity.AssignActivityToTrip(this); //Sets the Trip reference and TripId in the Activity
    }
}
