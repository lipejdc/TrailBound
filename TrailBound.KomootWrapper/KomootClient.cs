using System.Net.Http.Headers;
using System.Text.Json;
using TrailBound.KomootWrapper.Enums;
using TrailBound.KomootWrapper.Extensions;
using TrailBound.KomootWrapper.Interfaces;

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
}