using TrailBound.Domain.Enums;

namespace TrailBound.Domain.Entities;

public class Trip
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required TripCategory Categories { get; set; }
    public string? GoogleMapsUrl { get; set; }

    //Activities belonging to this trip
    public List<Activity> Activities { get; set; } = new(); 
}
