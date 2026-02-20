namespace TrailBound.Domain.Entities;

/// <summary>
/// Represents a geographical location for an Activity.
/// </summary>
public class Location
{
    public string? City { get; set; }
    public string? Area { get; set; }
    public string Country { get; set; } = null!;
}
