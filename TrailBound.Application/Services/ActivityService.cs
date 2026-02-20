using TrailBound.Application.Dtos.Activity;
using TrailBound.Application.Interfaces;

namespace TrailBound.Application.Services;

public class ActivityService(IActivityRepository activityRepository) : IActivityService
{
    private readonly IActivityRepository _activityRepository = activityRepository;

    public async Task<ReadActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto)
        => await _activityRepository.CreateActivityAsync(createActivityDto);

    public async Task<bool> DeleteActivityAsync(int id)
        => await _activityRepository.DeleteActivityAsync(id);

    public async Task<List<ReadActivityDto>> GetActivitiesAsync()
        => await _activityRepository.GetActivitiesAsync();

    public async Task<List<ReadActivityDto>> GetActivitiesByMonthAsync(int year, int month)
    {
        if (month < 1 || month > 12)
        {
            throw new ArgumentException("Month must be between 1 and 12!");
        }

        return await _activityRepository.GetActivitiesByMonthAsync(year, month);
    }

    public async Task<List<ReadActivityDto>> GetActivitiesByYearAsync(int year)
        => await _activityRepository.GetActivitiesByYearAsync(year);

    public async Task<ReadActivityDto?> GetActivityByIdAsync(int id)
        => await _activityRepository.GetActivityByIdAsync(id);

    public async Task<ReadActivityDto?> UpdateActivityAsync(int id, UpdateActivityDto updateActivityDto)
        => await _activityRepository.UpdateActivityAsync(id, updateActivityDto);
}
