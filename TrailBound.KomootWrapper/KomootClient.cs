using System.Net.Http.Headers;
using System.Text.Json;

namespace TrailBound.KomootWrapper;

public class KomootClient : IKomootClient
{
    private readonly HttpClient _httpClient;

    public KomootClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<KomootTour>> GetToursAsync(
        string userId,
        string bearerToken,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://www.komoot.com/api/v007/users/{userId}/tours"
            );

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

        var response = await _httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var tours = await JsonSerializer.DeserializeAsync<List<KomootTour>>(stream, options, cancellationToken);

        return tours ?? new List<KomootTour>();
    }
}
