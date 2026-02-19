using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TrailBound.Domain.Enums;

namespace TrailBound.Application.Dtos;

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

    [Required(ErrorMessage = "DistanceInKm is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Distance must be positive")]
    public double DistanceInKm { get; init; }

    public string? GpxFilePath { get; init; }
    public string? KomootUrl { get; init; }

    [Required(ErrorMessage = "Country is required")]
    public string Country { get; init; } = null!;
    public string? City { get; init; }
    public string? Region { get; init; }

    public string? TripName { get; init; }
}
