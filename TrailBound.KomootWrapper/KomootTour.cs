using System.Text.Json.Serialization;

namespace TrailBound.KomootWrapper;

public class KomootTour
{
    public string Id { get; set; }

    public string Type { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public DateTime Date { get; set; }

    [JsonPropertyName("kcal_active")]
    public int KcalActive { get; set; }

    [JsonPropertyName("kcal_resting")]
    public int KcalResting { get; set; }

    public StartPoint StartPoint { get; set; }

    public double Distance { get; set; }

    public int Duration { get; set; }

    [JsonPropertyName("elevation_up")]
    public double ElevationUp { get; set; }

    [JsonPropertyName("elevation_down")]
    public double ElevationDown { get; set; }

    public string Sport { get; set; }

    [JsonPropertyName("time_in_motion")]
    public int TimeInMotion { get; set; }

    [JsonPropertyName("changed_at")]
    public DateTime ChangedAt { get; set; }

    public MapImage MapImage { get; set; }

    public Embedded _embedded { get; set; }
}
