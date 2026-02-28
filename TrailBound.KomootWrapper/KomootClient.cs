using System.Net.Http.Headers;
using System.Text.Json;

namespace TrailBound.KomootWrapper;

public class KomootClient : IKomootClient
{
    private readonly HttpClient _httpClient;

    public KomootClient(HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri("https://www.komoot.com");
        _httpClient = httpClient;
    }

    public async Task<KomootTourResponse> GetToursAsync(
        string userId,
        string bearerToken,
        TourType type,
        string sortBy,
        string sortOrder,
        TourStatus status,
        int page,
        int limit,
        CancellationToken cancellationToken = default)
    {
        var url =
        $"/api/v007/users/{userId}/tours/" +
        $"?type={type.GetDescription()}" +
        $"&sort_field={sortBy}" +
        $"&sort_direction={sortOrder}" +
        $"&status={status.ToString().ToLower()}" +
        $"&page={page}" +
        $"&limit={limit}";

        var request = new HttpRequestMessage(HttpMethod.Get, url);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

        var response = await _httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

        // Read the entire response body as a string
        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            // Optional: if you want to preserve property names as-is
            // PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // Deserialize directly from stream
        var tours = await JsonSerializer.DeserializeAsync<KomootTourResponse>(
            stream, options, cancellationToken
        );

        return tours ?? new KomootTourResponse();
    }

    //public async Task LoginToKomootAsync(Dictionary<string, string> creds)
    //{
    //    if (!creds.ContainsKey("user_id") || !creds.ContainsKey("cookie"))
    //        throw new ArgumentException("Missing required credentials.");

    //    var userId = creds["user_id"];

    //    var rawCookies = creds["cookie"]?.Trim();
    //    if (string.IsNullOrEmpty(rawCookies))
    //        throw new ArgumentException("Cookie string cannot be empty.");

    //    var request = new HttpRequestMessage(
    //        HttpMethod.Get,
    //        "https://account.komoot.com/actions/transfer?type=signin"
    //    );

    //    // Inject raw cookie header manually
    //    request.Headers.Add("Cookie", rawCookies);

    //    var response = await _httpClient.SendAsync(request);

    //    response.EnsureSuccessStatusCode();

    //    // ✅ Read response body
    //    var responseBody = await response.Content.ReadAsStringAsync();

    //    // ✅ Read response cookies (if any)
    //    if (response.Headers.TryGetValues("Set-Cookie", out var setCookies))
    //    {
    //        foreach (var cookie in setCookies)
    //        {
    //            Console.WriteLine($"Set-Cookie: {cookie}");
    //        }
    //    }

    //    // Optional: log status
    //    Console.WriteLine($"Status: {(int)response.StatusCode}");
    //}
}
