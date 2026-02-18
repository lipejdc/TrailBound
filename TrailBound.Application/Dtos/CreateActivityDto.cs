namespace TrailBound.Application.Dtos;

public class CreateActivityDto
{
    public required string Title { get; set; }
    public required string Type { get; set; }
    public required string Status { get; set; }
    public required DateTime Date { get; set; }
    public required double DistanceInKm { get; set; }

    //Location flattened
    public required string Country { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }

    //Optional trip info
    public string? TripName { get; set; } //Optional convenience
}
