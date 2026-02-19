using System.Text.Json.Serialization;
using TrailBound.Domain.Enums;

namespace TrailBound.Application.Dtos;

public class ReadActivityDto
{
    public int Id { get; set; } //Needed for editing, deleting, navigation
    public string Title { get; set; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ActivityType Type { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ActivityStatus Status { get; set; }
    public DateTimeOffset Date { get; set; }
    public TimeSpan Duration { get; set; }
    public double DistanceInKm { get; set; }
    public int ElevationGain { get; set; }
    public int ElevationLoss { get; set; }
    
    //Location flattened
    public string Country { get; set; } = null!;
    public string? City { get; set; }
    public string? Region { get; set; }

    //Media / routes
    public string? GpxFilePath { get; set; }
    public string? KomootUrl { get; set; }

    //Optional trip info
    public int? TripId { get; set; } //Optional for linking to a trip
    public string? TripName { get; set; } //Optional convenience
}
