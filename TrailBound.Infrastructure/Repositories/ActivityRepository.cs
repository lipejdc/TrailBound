using Microsoft.EntityFrameworkCore;
using TrailBound.Application.Dtos;
using TrailBound.Application.Interfaces;
using TrailBound.Domain.Entities;
using TrailBound.Infrastructure.Persistence.DatabaseContext;

namespace TrailBound.Infrastructure.Repositories;

public class ActivityRepository(ApplicationDbContext context) : IActivityRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<ReadActivityDto>> GetActivitiesAsync()
    {
        var activities = await _context.Activities
            .Select(a => new ReadActivityDto
            {
                Id = a.Id,
                Title = a.Title,
                Type = a.Type,
                Status = a.Status,
                Date = a.Date,
                Duration = a.Duration,
                DistanceInKm = a.DistanceInKm,
                ElevationGain = a.ElevationGain,
                ElevationLoss = a.ElevationLoss,
                City = a.Location.City,
                Region = a.Location.Region,
                Country = a.Location.Country,
                GpxFilePath = a.GpxFilePath,
                KomootUrl = a.KomootUrl,
                TripId = a.TripId,
                TripName = a.Trip != null ? a.Trip.Name : null
            })
            .ToListAsync();

        return activities;
    }

    public async Task<ReadActivityDto?> GetActivityByIdAsync(int id)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null) return null;

        var dto = new ReadActivityDto
        {
            Id = activity.Id,
            Title = activity.Title,
            Type = activity.Type,
            Status = activity.Status,
            Date = activity.Date,
            Duration = activity.Duration,
            DistanceInKm = activity.DistanceInKm,
            ElevationGain = activity.ElevationGain,
            ElevationLoss = activity.ElevationLoss,
            GpxFilePath = activity.GpxFilePath,
            KomootUrl = activity.KomootUrl,
            TripName = activity.Trip?.Name,
            City = activity.Location.City,
            Region = activity.Location.Region,
            Country = activity.Location.Country
        };

        return dto;
    }

    public async Task<ReadActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto)
    {
        Location location = new()
        {
            Country = createActivityDto.Country,
            City = createActivityDto.City,
            Region = createActivityDto.Region
        };

        Activity activity = new()
        {
            Title = createActivityDto.Title,
            Type = createActivityDto.Type,
            Status = createActivityDto.Status,
            Date = createActivityDto.Date.ToUniversalTime(),
            DistanceInKm = createActivityDto.DistanceInKm,
            Location = location
        };

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
        var resultDto = new ReadActivityDto
        {
            Id = activity.Id,
            Title = activity.Title,
            Type = activity.Type,
            Status = activity.Status,
            Date = activity.Date,
            DistanceInKm = activity.DistanceInKm,
            Country = activity.Location.Country,
            City = activity.Location.City,
            Region = activity.Location.Region,
            TripName = activity.Trip?.Name
        };

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

    public async Task<ReadActivityDto?> EditActivityAsync(int id, UpdateActivityDto updatedActivity)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null)
        {
            return null;
        }

        activity.Title = updatedActivity.Title ?? activity.Title;
        activity.Type = updatedActivity.Type ?? activity.Type;
        activity.Status = updatedActivity.Status ?? activity.Status;
        activity.Date = updatedActivity.Date?.ToUniversalTime() ?? activity.Date;
        activity.DistanceInKm = updatedActivity.DistanceInKm ?? activity.DistanceInKm;
        activity.GpxFilePath = updatedActivity.GpxFilePath ?? activity.GpxFilePath;
        activity.KomootUrl = updatedActivity.KomootUrl ?? activity.KomootUrl;
        activity.Location.Country = updatedActivity.Country ?? activity.Location.Country;
        activity.Location.City = updatedActivity.City ?? activity.Location.City;
        activity.Location.Region = updatedActivity.Region ?? activity.Location.Region;

        await _context.SaveChangesAsync();

        return new ReadActivityDto
        {
            Id = activity.Id,
            Title = activity.Title,
            Type = activity.Type,
            Status = activity.Status,
            Date = activity.Date,
            DistanceInKm = activity.DistanceInKm,
            GpxFilePath = activity.GpxFilePath,
            KomootUrl = activity.KomootUrl,
            Country = activity.Location.Country,
            City = activity.Location.City,
            Region = activity.Location.Region,
        };
    }

    public async Task<List<ReadActivityDto>> GetActivitiesByMonthAsync(int year, int month)
    {
        var activities = await _context.Activities
        .Where(a => a.Date.Year == year && a.Date.Month == month)
        .Select(a => new ReadActivityDto
        {
            Id = a.Id,
            Title = a.Title,
            Type = a.Type,
            Status = a.Status,
            Date = a.Date,
            Duration = a.Duration,
            DistanceInKm = a.DistanceInKm,
            ElevationGain = a.ElevationGain,
            ElevationLoss = a.ElevationLoss,
            City = a.Location.City,
            Region = a.Location.Region,
            Country = a.Location.Country,
            GpxFilePath = a.GpxFilePath,
            KomootUrl = a.KomootUrl,
            TripId = a.TripId,
            TripName = a.Trip != null ? a.Trip.Name : null
        })
        .ToListAsync();

        return activities;
    }

    public async Task<List<ReadActivityDto>> GetActivitiesByYearAsync(int year)
    {
        var activities = await _context.Activities
        .Where(a => a.Date.Year == year)
        .Select(a => new ReadActivityDto
        {
            Id = a.Id,
            Title = a.Title,
            Type = a.Type,
            Status = a.Status,
            Date = a.Date,
            Duration = a.Duration,
            DistanceInKm = a.DistanceInKm,
            ElevationGain = a.ElevationGain,
            ElevationLoss = a.ElevationLoss,
            City = a.Location.City,
            Region = a.Location.Region,
            Country = a.Location.Country,
            GpxFilePath = a.GpxFilePath,
            KomootUrl = a.KomootUrl,
            TripId = a.TripId,
            TripName = a.Trip != null ? a.Trip.Name : null
        })
        .ToListAsync();

        return activities;
    }
}
