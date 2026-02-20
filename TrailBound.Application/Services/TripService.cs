using TrailBound.Application.Dtos.Trip;
using TrailBound.Application.Interfaces;

namespace TrailBound.Application.Services;

public class TripService(ITripRepository tripRepository) : ITripService
{

    private readonly ITripRepository _tripRepository = tripRepository;

    public async Task<ReadTripDto> CreateTripAsync(CreateTripDto createTripDto)
        => await _tripRepository.CreateTripAsync(createTripDto);

    public async Task<bool> DeleteTripAsync(int id)
        => await _tripRepository.DeleteTripAsync(id);

    public async Task<ReadTripDto?> GetTripByIdAsync(int id)
        => await _tripRepository.GetTripByIdAsync(id);

    public async Task<List<ReadTripDto>> GetTripsAsync()
        => await _tripRepository.GetTripsAsync();

    public async Task<List<ReadTripDto>> GetTripsByYearAsync(int year)
        => await _tripRepository.GetTripsByYearAsync(year);

    public async Task<ReadTripDto?> UpdateTripAsync(int id, UpdateTripDto updateTripDto)
        => await _tripRepository.UpdateTripAsync(id, updateTripDto);
}
