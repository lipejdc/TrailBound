using Microsoft.EntityFrameworkCore;
using TrailBound.Application.Dtos.Activity;
using TrailBound.Application.Interfaces;
using TrailBound.Application.Mappers;
using TrailBound.Domain.Entities;
using TrailBound.Infrastructure.Persistence.DatabaseContext;

namespace TrailBound.Infrastructure.Repositories;

public class ActivityRepository(ApplicationDbContext context) : IActivityRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<ReadActivityDto>> GetActivitiesAsync()
    {
        var activities = await _context.Activities
            .Select(a => a.ToReadDto())
            .ToListAsync();

        return activities;
    }

    public async Task<ReadActivityDto?> GetActivityByIdAsync(int id)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null) return null;

        var dto = activity.ToReadDto();

        return dto;
    }

    public async Task<ReadActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto)
    {
        var activity = createActivityDto.ToEntity();

        //Assign trip if TripName or TripId is provided
        if (!string.IsNullOrEmpty(createActivityDto.TripName))
        {
            var trip = await _context.Trips.FirstOrDefaultAsync(t => t.Name == createActivityDto.TripName);
            if (trip != null)
            {
                activity.AssignActivityToTrip(trip); //Sets Trip and TripId internally
            }
        }

        _context.Activities.Add(activity);

        await _context.SaveChangesAsync();

        //Map entity → ActivityDto for returning
        var resultDto = activity.ToReadDto();

        return resultDto;
    }

    public async Task<bool> DeleteActivityAsync(int id)
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

    public async Task<ReadActivityDto?> UpdateActivityAsync(int id, UpdateActivityDto updateActivityDto)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null)
        {
            return null;
        }

        activity.ApplyUpdate(updateActivityDto);

        await _context.SaveChangesAsync();

        return activity.ToReadDto();
    }

    public async Task<List<ReadActivityDto>> GetActivitiesByMonthAsync(int year, int month)
    {
        var activities = await _context.Activities
        .Where(a => a.Date.Year == year && a.Date.Month == month)
        .Select(a => a.ToReadDto())
        .ToListAsync();

        return activities;
    }

    public async Task<List<ReadActivityDto>> GetActivitiesByYearAsync(int year)
    {
        var activities = await _context.Activities
        .Where(a => a.Date.Year == year)
        .Select(a => a.ToReadDto())
        .ToListAsync();

        return activities;
    }

    public async Task<Activity?> GetByIdAsync(int id)
    {
        return await _context.Activities
            .Include(a => a.Trip)       // include navigation property
            .Include(a => a.Location)   // include location
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}
