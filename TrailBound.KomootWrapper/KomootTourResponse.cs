using System.Text.Json.Serialization;

public class KomootTourResponse
{
    [JsonPropertyName("_embedded")]
    public Embedded? Embedded { get; set; }

    [JsonPropertyName("_links")]
    public Links? Links { get; set; }

    [JsonPropertyName("page")]
    public PageInfo? Page { get; set; }
}

public class Embedded
{
    [JsonPropertyName("tours")]
    public List<KomootTour>? Tours { get; set; }
}

public class Links
{
    [JsonPropertyName("self")]
    public Link? Self { get; set; }

    [JsonPropertyName("next")]
    public Link? Next { get; set; }
}

public class Link
{
    [JsonPropertyName("href")]
    public string? Href { get; set; }
}

public class PageInfo
{
    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("totalElements")]
    public int TotalElements { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("number")]
    public int Number { get; set; }
}

public class StartPoint
{
    [JsonPropertyName("lat")]
    public double Latitude { get; set; }

    [JsonPropertyName("lng")]
    public double Longitude { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

public class MapImage
{
    [JsonPropertyName("src")]
    public string? Src { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }
}

public class KomootTour
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("sport")]
    public string? Sport { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("distance")]
    public double Distance { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("start_point")]
    public StartPoint? StartPoint { get; set; }

    [JsonPropertyName("map_image")]
    public MapImage? MapImage { get; set; }
}