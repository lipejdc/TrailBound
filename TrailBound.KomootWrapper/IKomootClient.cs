namespace TrailBound.KomootWrapper;

public interface IKomootClient
{
    Task<KomootTourResponse> GetToursAsync(
        string userId,
        string bearerToken,
        TourType type,
        string sortBy,
        string sortOrder,
        TourStatus status,
        int page,
        int limit,
        CancellationToken cancellationToken);

    //Task LoginToKomootAsync(Dictionary<string, string> creds);
}
