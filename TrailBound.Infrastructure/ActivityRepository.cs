using TrailBound.Application.Dtos;
using TrailBound.Application.Interfaces;
using TrailBound.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace TrailBound.Infrastructure;

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

    public Task<ActivityDto> CreateActivityAsync(ActivityDto activityDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteActivity(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ActivityDto?> EditActivityAsync(int id, ActivityDto updatedActivity)
    {
        throw new NotImplementedException();
    }

    public Task<List<ActivityDto>> GetActivitiesByMonthAsync(int year, int month)
    {
        throw new NotImplementedException();
    }

    public Task<List<ActivityDto>> GetActivitiesByYearAsync(int year)
    {
        throw new NotImplementedException();
    }

    public Task<ActivityDto?> GetActivityByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
