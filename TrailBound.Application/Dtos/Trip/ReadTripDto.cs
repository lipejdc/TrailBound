using System.Text.Json.Serialization;
using TrailBound.Domain.Enums;

namespace TrailBound.Application.Dtos.Trip;

public record ReadTripDto
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TripCategory Categories { get; set; }

    // Location flattened
    public string Country { get; init; } = null!;
    public string? City { get; init; }
    public string? Area { get; init; }
    public string? GoogleMapsUrl { get; init; }
}
