using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TrailBound.Domain.Enums;

namespace TrailBound.Application.Dtos;

public class UpdateActivityDto
{
    public string? Title { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ActivityType? Type { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ActivityStatus? Status { get; set; }
    public DateTimeOffset? Date { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Distance must be positive")]
    public double? DistanceInKm { get; set; }
    public string? GpxFilePath { get; set; }
    public string? KomootUrl { get; set; }

    //Location flattened
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }

    //Optional trip info
    public string? TripName { get; set; } //Optional convenience
}
