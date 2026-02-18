namespace TrailBound.Application.Dtos;

public class ActivityDto
{
    public int Id { get; set; } //Needed for editing, deleting, navigation
    public required string Title { get; set; }
    public required string Type { get; set; }
    public required string Status { get; set; }
    public required DateTime Date { get; set; }
    public TimeSpan Duration { get; set; }
    public required double DistanceInKm { get; set; }
    public int ElevationGain { get; set; }
    public int ElevationLoss { get; set; }
    
    //Location flattened
    public required string Country { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }

    //Media / routes
    public string? GpxFilePath { get; set; }
    public string? KomootUrl { get; set; }

    //Optional trip info
    public int? TripId { get; set; } //Optional for linking to a trip
    public string? TripName { get; set; } //Optional convenience
}
