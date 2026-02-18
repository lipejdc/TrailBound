using TrailBound.Application.Dtos;

namespace TrailBound.Application.Interfaces;

public interface IActivityRepository
{
    Task<List<ActivityDto>> GetActivitiesAsync();
    Task<List<ActivityDto>> GetActivitiesByYearAsync(int year);
    Task<List<ActivityDto>> GetActivitiesByMonthAsync(int year, int month);
    Task<ActivityDto?> GetActivityByIdAsync(int id);
    Task<ActivityDto> CreateActivityAsync(CreateActivityDto activityDto);
    Task<ActivityDto?> EditActivityAsync(int id, ActivityDto updatedActivity);
    Task<bool> DeleteActivity(int id);

}
