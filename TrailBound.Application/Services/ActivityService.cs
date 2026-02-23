using TrailBound.Application.Dtos.Activity;
using TrailBound.Application.Interfaces;
using TrailBound.Application.Mappers;

namespace TrailBound.Application.Services;

public class ActivityService(IActivityRepository activityRepository) : IActivityService
{
    private readonly IActivityRepository _activityRepository = activityRepository;


    public async Task<List<ReadActivityDto>> GetActivitiesAsync()
    {
        var activities = await _activityRepository.GetAllAsync();

        return activities.Select(a => a.ToReadDto()).ToList();
    }

    public async Task<ReadActivityDto?> GetActivityByIdAsync(int id)
    {
        var activity = await _activityRepository.GetByIdAsync(id);

        return activity?.ToReadDto();
    }

    public async Task<List<ReadActivityDto>> GetActivitiesByMonthAsync(int year, int month)
    {
        if (month < 1 || month > 12)
        {
            throw new ArgumentException("Month must be between 1 and 12!");
        }

        var activities = await _activityRepository.GetByMonthAsync(year, month);

        return activities.Select(a => a.ToReadDto()).ToList();
    }

    public async Task<List<ReadActivityDto>> GetActivitiesByYearAsync(int year)
    {
        var activities = await _activityRepository.GetByYearAsync(year);

        return activities.Select(a => a.ToReadDto()).ToList();
    }

    public async Task<ReadActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto)
    {
        var entity = createActivityDto.ToEntity();

        var createdEntity = await _activityRepository.CreateAsync(entity);

        return createdEntity.ToReadDto();
    }

    public async Task<bool> DeleteActivityAsync(int id)
        => await _activityRepository.DeleteAsync(id);

    public async Task<ReadActivityDto?> UpdateActivityAsync(int id, UpdateActivityDto updateActivityDto)
    {
        var existingActivity = await _activityRepository.GetByIdAsync(id);

        if (existingActivity == null)
        {
            return null;
        }

        existingActivity.ApplyUpdate(updateActivityDto);

        var updatedActivity = await _activityRepository.UpdateAsync(existingActivity);

        return updatedActivity?.ToReadDto();
    }
}
