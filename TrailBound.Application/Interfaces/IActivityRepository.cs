using TrailBound.Application.Dtos.Activity;
using TrailBound.Domain.Entities;

namespace TrailBound.Application.Interfaces;

public interface IActivityRepository
{
    Task<List<ReadActivityDto>> GetActivitiesAsync();
    Task<List<ReadActivityDto>> GetActivitiesByYearAsync(int year);
    Task<List<ReadActivityDto>> GetActivitiesByMonthAsync(int year, int month);
    Task<ReadActivityDto?> GetActivityByIdAsync(int id);
    Task<ReadActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto);
    Task<ReadActivityDto?> UpdateActivityAsync(int id, UpdateActivityDto updateActivityDto);
    Task<bool> DeleteActivityAsync(int id);
    Task<Activity?> GetByIdAsync(int id);

}
