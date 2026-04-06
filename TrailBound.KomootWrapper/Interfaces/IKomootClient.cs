using TrailBound.KomootWrapper.Enums;

namespace TrailBound.KomootWrapper.Interfaces;

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
}
