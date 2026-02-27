namespace TrailBound.KomootWrapper;

public interface IKomootClient
{
    Task<List<KomootTour>> GetToursAsync(string userId,
        string bearerToken,
        CancellationToken cancellationToken);
}
