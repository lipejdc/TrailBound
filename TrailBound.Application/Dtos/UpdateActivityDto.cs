using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TrailBound.Domain.Enums;

namespace TrailBound.Application.Dtos;

public record UpdateActivityDto
{
    public string? Title { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ActivityType? Type { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ActivityStatus? Status { get; init; }

    public DateTimeOffset? Date { get; init; }

    [Range(0, double.MaxValue, ErrorMessage = "Distance must be positive")]
    public double? DistanceInKm { get; init; }

    public string? GpxFilePath { get; init; }
    public string? KomootUrl { get; init; }

    // Location flattened
    public string? Country { get; init; }
    public string? City { get; init; }
    public string? Region { get; init; }

    // Optional trip info
    public string? TripName { get; init; }
}
