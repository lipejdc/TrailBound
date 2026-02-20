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
        activity.AssignActivityToTrip(this); //Sets activity.Trip = trip and activity.TripId = trip.Id
    }

    public void RemoveActivityFromTrip(Activity activity)
    {
        ArgumentNullException.ThrowIfNull(activity);

        if (_activities.Remove(activity)) //Check if activity was removed, if yes...
        {
            if (activity.Trip == this) //..then check if the activity.Trip points to this trip...
                activity.AssignActivityToTrip(null); //Sets activity.Trip = null and activity.TripId = null
        }
    }
}
