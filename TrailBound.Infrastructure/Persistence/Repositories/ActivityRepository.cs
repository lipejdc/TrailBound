using Microsoft.EntityFrameworkCore;
using TrailBound.Application.Dtos.Pagination;
using TrailBound.Application.Interfaces;
using TrailBound.Domain.Entities;
using TrailBound.Domain.Enums;
using TrailBound.Infrastructure.Persistence.DatabaseContext;

namespace TrailBound.Infrastructure.Repositories;

public class ActivityRepository(ApplicationDbContext context) : IActivityRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<PaginatedResult<Activity>> GetAllAsync(
        ActivityPaginationFilter paginationFilter,
        CancellationToken cancellationToken = default)
    {
        var activities = _context.Activities
        .Include(a => a.Trip)
        .AsNoTracking()
        .AsQueryable();

        if (!string.IsNullOrEmpty(paginationFilter.Title))
        {
            activities = activities.Where(a => a.Title.Contains(paginationFilter.Title));
        }

        if (!string.IsNullOrEmpty(paginationFilter.ActivityType))
        {
            activities = activities.Where(a => a.Type.
            ToString().
            ToLower().
            Contains(paginationFilter.ActivityType));
        }

        if (paginationFilter.StartDate != DateTimeOffset.MinValue && paginationFilter.EndDate != DateTimeOffset.MaxValue)
        {
            activities = activities.Where(a => a.Date >= paginationFilter.StartDate 
            && a.Date <= paginationFilter.EndDate);
        }

        if (paginationFilter.MinimalDuration != TimeSpan.MinValue && paginationFilter.MaximalDuration != TimeSpan.MaxValue)
        {
            activities = activities.Where(a => a.Duration >= paginationFilter.MinimalDuration 
            && a.Duration <= paginationFilter.MaximalDuration);
        }

        if (paginationFilter.MinimalDistanceInMeters > 0 && paginationFilter.MaximalDistanceInMeters < int.MaxValue)
        {
            activities = activities.Where(a => a.Route.DistanceInMeters >= paginationFilter.MinimalDistanceInMeters
            && a.Route.DistanceInMeters <= paginationFilter.MaximalDistanceInMeters);
        }

        var count = activities.Count();

        if (count <= 0)
        {
            return new PaginatedResult<Activity>(
                new List<Activity>(),
                paginationFilter.PageNumber,
                paginationFilter.PageSize,
                count);
        }

        var paginatedActivities = activities
            .Skip(paginationFilter.GetSkipCount())
            .Take(paginationFilter.PageSize)
            .ToList();

        return new PaginatedResult<Activity>(
            paginatedActivities, 
            paginationFilter.PageNumber, 
            paginationFilter.PageSize, 
            count
            );
    }

    public async Task<Activity?> GetByIdAsync(int id)
    {
        var activity = await _context.Activities.FindAsync(id);

        return activity;
    }

    public async Task<List<Activity>> GetByMonthAsync(int year, int month)
    {
        var activities = await _context.Activities
        .Where(a => a.Date.Year == year && a.Date.Month == month)
        .ToListAsync();

        return activities;
    }

    public async Task<List<Activity>> GetByYearAsync(int year)
    {
        var activities = await _context.Activities
        .Where(a => a.Date.Year == year)
        .ToListAsync();

        return activities;
    }

    public async Task<Activity> CreateAsync(Activity activity)
    {
        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();

        return activity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null)
        {
            return false;
        }

        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<Activity?> UpdateAsync(Activity activity)
    {
  
        await _context.SaveChangesAsync();

        return activity;
    }
}
