using System.ComponentModel.DataAnnotations;

namespace TrailBound.Application.Dtos;

public class UpdateActivityDto
{
    public string? Title { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public DateTime? Date { get; set; }

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
