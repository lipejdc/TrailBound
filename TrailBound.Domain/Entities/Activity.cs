using TrailBound.Domain.Enums;

namespace TrailBound.Domain.Entities;

public class Activity
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required ActivityType Type { get; set; }
    public required Location Location { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Duration { get; set; }
    public double DistanceInKm { get; set; }
    public int ElevationGain { get; set; }
    public int ElevationLoss { get; set; }
    public required string GpxFilePath { get; set; }
    public required string KomootUrl { get; set; }
    public required ActivityStatus Status { get; set; }
}
