namespace TrailBound.Domain.Entities;

/// <summary>
/// Represents a geographical location for an Activity.
/// </summary>
public class Location
{
    public string? City { get; private set; }
    public string? Region { get; private set; }
    public string Country { get; private set; }

    public Location(string country, string? city = null, string? region = null)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            throw new ArgumentNullException("Country cannot be empty!", nameof(country));
        }

        Country = country;
        City = city;
        Region = region;

    }
}
