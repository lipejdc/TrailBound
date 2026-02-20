using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TrailBound.Domain.Enums;

namespace TrailBound.Application.Dtos.Trip;

public record CreateTripDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; init; } = null!;
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }

    [Required(ErrorMessage = "TripCategory is required")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TripCategory Categories { get; set; }

    // Location flattened
    [Required(ErrorMessage = "Country is required")]
    public string Country { get; init; } = null!;
    public string? City { get; init; }
    public string? Area { get; init; }
    public string? GoogleMapsUrl { get; init; }
}
