namespace TrailBound.Domain.Entities;

/// <summary>
/// Represents the physical path of an Activity.
/// </summary>

public class Route
{
    public string StartPoint { get; set; } = string.Empty;
    public string EndPoint { get; set; } = string.Empty;
    public double DistanceInKm { get; set; }
    public int ElevationGain { get; set; }
    public int ElevationLoss { get; set; }
}
