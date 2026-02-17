namespace TrailBound.Domain.Enums;

[Flags]
public enum TripCategory
{
    None = 0,
    Hiking = 1,
    Cycling = 2,
    Mountaineering = 4,
    Tourism = 8
}
