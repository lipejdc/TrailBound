using Microsoft.EntityFrameworkCore;
using TrailBound.Application.Dtos;
using TrailBound.Application.Helpers;
using TrailBound.Application.Interfaces;
using TrailBound.Domain.Entities;
using TrailBound.Infrastructure.Persistence.DatabaseContext;

namespace TrailBound.Infrastructure.Repositories;

public class ActivityRepository(ApplicationDbContext context) : IActivityRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<ActivityDto>> GetActivitiesAsync()
    {
        var activities = await _context.Activities
            .Select(a => new ActivityDto
            {
                Id = a.Id,
                Title = a.Title,
                Type = a.Type.ToString(),
                Status = a.Status.ToString(),
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

    public async Task<ActivityDto?> GetActivityByIdAsync(int id)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null) return null;

        var dto = new ActivityDto
        {
            Id = activity.Id,
            Title = activity.Title,
            Type = activity.Type.ToString(),
            Status = activity.Status.ToString(),
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

    public async Task<ActivityDto> CreateActivityAsync(CreateActivityDto activityDto)
    {
        Location location = new()
        {
            Country = activityDto.Country,
            City = activityDto.City,
            Region = activityDto.Region
        };

        var type = EnumHelpers.ParseActivityType(activityDto.Type);
        var status = EnumHelpers.ParseActivityStatus(activityDto.Status);


        Activity activity = new()
        {
            Title = activityDto.Title,
            Type = type,
            Status = status,
            Date = DateTime.SpecifyKind(activityDto.Date, DateTimeKind.Utc),
            DistanceInKm = activityDto.DistanceInKm,
            Location = location
        };

        if (!string.IsNullOrEmpty(activityDto.TripName))
        {
            var trip = await _context.Trips.FirstOrDefaultAsync(t => t.Name == activityDto.TripName);
            if (trip != null)
            {
                activity.AssignActivityToTrip(trip); //Sets Trip and TripId internally
            }
        }

        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();

        //Map entity → ActivityDto for returning
        var resultDto = new ActivityDto
        {
            Id = activity.Id,
            Title = activity.Title,
            Type = activity.Type.ToString(),
            Status = activity.Status.ToString(),
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

    public async Task<ActivityDto?> EditActivityAsync(int id, UpdateActivityDto updatedActivity)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null)
        {
            return null;
        }

        activity.Title = updatedActivity.Title ?? activity.Title;
        activity.Type = !string.IsNullOrEmpty(updatedActivity.Type)
            ? EnumHelpers.ParseActivityType(updatedActivity.Type)
            : activity.Type;
        activity.Status = !string.IsNullOrEmpty(updatedActivity.Status)
            ? EnumHelpers.ParseActivityStatus(updatedActivity.Status)
            : activity.Status;
        activity.Date = updatedActivity.Date ?? activity.Date;
        activity.DistanceInKm = updatedActivity.DistanceInKm ?? activity.DistanceInKm;
        activity.GpxFilePath = updatedActivity.GpxFilePath ?? activity.GpxFilePath;
        activity.KomootUrl = updatedActivity.KomootUrl ?? activity.KomootUrl;
        activity.Location.Country = updatedActivity.Country ?? activity.Location.Country;
        activity.Location.City = updatedActivity.City ?? activity.Location.City;
        activity.Location.Region = updatedActivity.Region ?? activity.Location.Region;

        await _context.SaveChangesAsync();

        return new ActivityDto
        {
            Id = activity.Id,
            Title = activity.Title,
            Type = activity.Type.ToString(),
            Status = activity.Status.ToString(),
            Date = activity.Date,
            DistanceInKm = activity.DistanceInKm,
            GpxFilePath = activity.GpxFilePath,
            KomootUrl = activity.KomootUrl,
            Country = activity.Location.Country,
            City = activity.Location.City,
            Region = activity.Location.Region,
        };
    }

    public async Task<List<ActivityDto>> GetActivitiesByMonthAsync(int year, int month)
    {
        var activities = await _context.Activities
        .Where(a => a.Date.Year == year && a.Date.Month == month)
        .Select(a => new ActivityDto
        {
            Id = a.Id,
            Title = a.Title,
            Type = a.Type.ToString(),
            Status = a.Status.ToString(),
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

    public async Task<List<ActivityDto>> GetActivitiesByYearAsync(int year)
    {
        var activities = await _context.Activities
        .Where(a => a.Date.Year == year)
        .Select(a => new ActivityDto
        {
            Id = a.Id,
            Title = a.Title,
            Type = a.Type.ToString(),
            Status = a.Status.ToString(),
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
