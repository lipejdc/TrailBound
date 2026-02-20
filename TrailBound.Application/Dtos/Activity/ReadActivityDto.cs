using System.Text.Json.Serialization;
using TrailBound.Application.Dtos.Route;
using TrailBound.Domain.Enums;

namespace TrailBound.Application.Dtos.Activity;

public record ReadActivityDto
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ActivityType Type { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ActivityStatus Status { get; init; }
    public DateTimeOffset Date { get; init; }
    public TimeSpan Duration { get; init; }
    public ReadRouteDto Route { get; init; } = new();

    // Location flattened
    public string Country { get; init; } = null!;
    public string? City { get; init; }
    public string? Area { get; init; }

    // Media / routes
    public string? GpxFilePath { get; init; }
    public string? KomootUrl { get; init; }

    // Optional trip info
    public int? TripId { get; init; }
    public string? TripName { get; init; }
}
