using TrailBound.Domain.Enums;

namespace TrailBound.Domain.Entities;

/// <summary>
/// Represents a Trip, which can have zero or more Activities.
/// </summary>

public class Trip
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public TripCategory Categories { get; private set; }
    public string? GoogleMapsUrl { get; private set; }

    //Activities belonging to this trip
    private readonly List<Activity> _activities = new();
    public IReadOnlyList<Activity> Activities => _activities.AsReadOnly();

    private Trip() //Private constructor for EF Core
    {
        Name = null!;
    }

    public Trip(string name, DateTime startDate, DateTime endDate, TripCategory categories)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        Categories = categories;
    }

    public void AddActivityToTrip(Activity activity)
    {
        ArgumentNullException.ThrowIfNull(activity);
        if (_activities.Contains(activity)) return;

        _activities.Add(activity);
        activity.AssignActivityToTrip(this); //Sets the Trip reference and TripId in the Activity
    }
}
