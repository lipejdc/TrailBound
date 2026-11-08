using Microsoft.EntityFrameworkCore;
using TrailBound.Application.Dtos.Pagination;
using TrailBound.Application.Interfaces;
using TrailBound.Domain.Entities;
using TrailBound.Infrastructure.Persistence.DatabaseContext;

namespace TrailBound.Infrastructure.Repositories;

public class ActivityRepository(ApplicationDbContext context) : IActivityRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Activity>> GetAllAsync(
    ActivityPaginationFilter filter,
    CancellationToken cancellationToken = default)
    {
        var query = _context.Activities
            .Include(a => a.Trip)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Title))
        {
            query = query.Where(a => EF.Functions.ILike(a.Title, $"%{filter.Title}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.ActivityType))
        {
            var type = filter.ActivityType.ToLower();
            query = query.Where(a => a.Type.ToString().ToLower().Contains(type));
        }

        if (filter.StartDate is { } start)
        {
            var startUtc = start.UtcDateTime;
            query = query.Where(a => a.Date >= startUtc);
        }

        if (filter.EndDate is { } end)
        {
            var endUtc = end.UtcDateTime;
            query = query.Where(a => a.Date <= endUtc);
        }

        if (filter.MinimalDuration is { } minDuration)
            query = query.Where(a => a.Duration >= minDuration);

        if (filter.MaximalDuration is { } maxDuration)
            query = query.Where(a => a.Duration <= maxDuration);

        if (filter.MinimalDistanceInMeters is { } minDistance)
            query = query.Where(a => a.Route.DistanceInMeters >= minDistance);

        if (filter.MaximalDistanceInMeters is { } maxDistance)
            query = query.Where(a => a.Route.DistanceInMeters <= maxDistance);

        return await query
            .OrderByDescending(a => a.Date)
            .Skip(filter.GetSkipCount())
            .Take(filter.PageSize)
            .ToListAsync(cancellationToken);
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
