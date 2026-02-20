using TrailBound.Application.Dtos.Trip;

namespace TrailBound.Application.Interfaces;

public interface ITripService
{
    Task<List<ReadTripDto>> GetTripsAsync();
    Task<List<ReadTripDto>> GetTripsByYearAsync(int year);
    Task<ReadTripDto?> GetTripByIdAsync(int id);
    Task<ReadTripDto> CreateTripAsync(CreateTripDto createTripDto);
    Task<ReadTripDto?> UpdateTripAsync(int id, UpdateTripDto updateTripDto);
    Task<bool> DeleteTripAsync(int id);
}
