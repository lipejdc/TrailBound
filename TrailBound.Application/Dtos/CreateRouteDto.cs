using System.ComponentModel.DataAnnotations;

namespace TrailBound.Application.Dtos;

public record CreateRouteDto
{
    [Required]
    public string StartPoint { get; init; } = string.Empty;

    [Required]
    public string EndPoint { get; init; } = string.Empty;

    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "Distance must be greater than 0")]
    public double DistanceInKm { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Elevation gain must be greater than 0")]
    public int ElevationGain { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Elevation gain must be greater than 0")]
    public int ElevationLoss { get; init; }
}
