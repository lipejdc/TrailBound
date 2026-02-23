using TrailBound.Domain.Entities;

namespace TrailBound.Application.Interfaces;

public interface IActivityRepository
{
    Task<List<Activity>> GetAllAsync();
    Task<Activity?> GetByIdAsync(int id);
    Task<List<Activity>> GetByYearAsync(int year);
    Task<List<Activity>> GetByMonthAsync(int year, int month);
    Task<Activity> CreateAsync(Activity activity);
    Task<bool> DeleteAsync(int id);
    Task<Activity?> UpdateAsync(Activity activity);
}
