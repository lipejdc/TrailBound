using TrailBound.Application.Dtos.Activity;

namespace TrailBound.Application.Interfaces
{
    public interface IActivityService
    {
        Task<List<ReadActivityDto>> GetActivitiesAsync();
        Task<ReadActivityDto?> GetActivityByIdAsync(int id);
        Task<ReadActivityDto> CreateActivityAsync(CreateActivityDto dto);
        Task<ReadActivityDto?> UpdateActivityAsync(int id, UpdateActivityDto dto);
        Task<bool> DeleteActivityAsync(int id);
        Task<List<ReadActivityDto>> GetActivitiesByYearAsync(int year);
        Task<List<ReadActivityDto>> GetActivitiesByMonthAsync(int year, int month);
        //Task AssignActivityToTripAsync(int activityId, int tripId);
        //Task RemoveActivityFromTripAsync(int activityId);
    }
}
