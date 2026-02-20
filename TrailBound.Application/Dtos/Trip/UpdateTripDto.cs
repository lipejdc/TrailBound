using System.Text.Json.Serialization;
using TrailBound.Domain.Enums;

namespace TrailBound.Application.Dtos.Trip;

public record UpdateTripDto
{
    public string? Title { get; init; }
    public DateTimeOffset? StartDate { get; init; }
    public DateTimeOffset? EndDate { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TripCategory? Categories { get; set; }

    // Location flattened
    public string? Country { get; init; }
    public string? City { get; init; }
    public string? Area { get; init; }
    public string? GoogleMapsUrl { get; init; }
}
