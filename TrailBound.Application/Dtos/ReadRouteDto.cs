using System.ComponentModel.DataAnnotations;

namespace TrailBound.Application.Dtos;

public record ReadRouteDto
{
    public string StartPoint { get; init; } = string.Empty;
    public string EndPoint { get; init; } = string.Empty;
    public double DistanceInKm { get; init; }
    public int ElevationGain { get; init; }
    public int ElevationLoss { get; init; }
}
