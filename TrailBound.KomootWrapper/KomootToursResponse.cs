using System.Text.Json;

namespace TrailBound.KomootWrapper;

public class KomootToursResponse
{
    public int Page { get; set; }
    public int Total { get; set; }

    public Embedded Embedded { get; set; }
}