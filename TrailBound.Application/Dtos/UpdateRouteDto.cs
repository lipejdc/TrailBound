namespace TrailBound.Application.Dtos;

public record UpdateRouteDto
{
    public string? StartPoint { get; init; }
    public string? EndPoint { get; init; }
    public double? DistanceInKm { get; init; }
    public int? ElevationGain { get; init; }
    public int? ElevationLoss { get; init; }
}
