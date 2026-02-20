using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TrailBound.Application.Dtos.Route;
using TrailBound.Domain.Enums;

namespace TrailBound.Application.Dtos.Activity;

public record CreateActivityDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; init; } = null!;

    [Required(ErrorMessage = "Type is required")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ActivityType Type { get; init; }

    [Required(ErrorMessage = "Status is required")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ActivityStatus Status { get; init; }

    [Required(ErrorMessage = "Date is required")]
    public DateTimeOffset Date { get; init; }
    public CreateRouteDto Route { get; init; } = new();
    public string? GpxFilePath { get; init; }
    public string? KomootUrl { get; init; }

    [Required(ErrorMessage = "Country is required")]
    public string Country { get; init; } = null!;
    public string? City { get; init; }
    public string? Region { get; init; }

    public int? TripId { get; init; }

    //Optional, for assigning to existing trip
    public string? TripName { get; init; }
}
