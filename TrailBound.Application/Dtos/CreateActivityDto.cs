using System.ComponentModel.DataAnnotations;

namespace TrailBound.Application.Dtos;

public class CreateActivityDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Type is required")]
    public string Type { get; set; } = null!;

    [Required(ErrorMessage = "Status is required")]
    public string Status { get; set; } = null!;

    [Required(ErrorMessage = "Date is required")]
    public DateTime Date { get; set; }

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
