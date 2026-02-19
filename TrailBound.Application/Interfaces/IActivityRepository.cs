using TrailBound.Application.Dtos;

namespace TrailBound.Application.Interfaces;

public interface IActivityRepository
{
    Task<List<ReadActivityDto>> GetActivitiesAsync();
    Task<List<ReadActivityDto>> GetActivitiesByYearAsync(int year);
    Task<List<ReadActivityDto>> GetActivitiesByMonthAsync(int year, int month);
    Task<ReadActivityDto?> GetActivityByIdAsync(int id);
    Task<ReadActivityDto> CreateActivityAsync(CreateActivityDto activityDto);
    Task<ReadActivityDto?> EditActivityAsync(int id, UpdateActivityDto updatedActivity);
    Task<bool> DeleteActivityAsync(int id);

}
