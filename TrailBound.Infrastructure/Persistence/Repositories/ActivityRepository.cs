using Microsoft.EntityFrameworkCore;
using TrailBound.Application.Interfaces;
using TrailBound.Domain.Entities;
using TrailBound.Infrastructure.Persistence.DatabaseContext;

namespace TrailBound.Infrastructure.Repositories;

public class ActivityRepository(ApplicationDbContext context) : IActivityRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Activity>> GetAllAsync()
    {
        var activities = await _context.Activities
        .Include(a => a.Trip)
        .ToListAsync();

        return activities;
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
