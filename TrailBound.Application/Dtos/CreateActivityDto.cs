using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TrailBound.Domain.Enums;

namespace TrailBound.Application.Dtos;

public class CreateActivityDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Type is required")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ActivityType Type { get; set; }

    [Required(ErrorMessage = "Status is required")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ActivityStatus Status { get; set; }

    [Required(ErrorMessage = "Date is required")]
    public DateTimeOffset Date { get; set; }

    [Required(ErrorMessage = "DistanceInKm is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Distance must be positive")]
    public double DistanceInKm { get; set; }
    public string? GpxFilePath { get; set; }
    public string? KomootUrl { get; set; }

    //Location flattened

    [Required(ErrorMessage = "Country is required")]
    public string Country { get; set; } = null!;
    public string? City { get; set; }
    public string? Region { get; set; }

    //Optional trip info
    public string? TripName { get; set; }
}
