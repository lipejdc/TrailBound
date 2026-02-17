namespace TrailBound.Domain.Entities;

//Value object, does not need ID.
public class Location
{
    public string? City { get; set; }
    public string? Region { get; set; }
    public required string Country { get; set; }
}
