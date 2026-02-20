using TrailBound.Application.Dtos;
using TrailBound.Domain.Entities;

namespace TrailBound.Application.Mappers;

public static class ActivityMapper
{
    public static ReadActivityDto ToReadDto(this Activity activity)
    {
        return new ReadActivityDto
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
            City = activity.Location.City,
            Region = activity.Location.Region,
            Country = activity.Location.Country,
            GpxFilePath = activity.GpxFilePath,
            KomootUrl = activity.KomootUrl,
            TripId = activity.TripId,
            TripName = activity.Trip?.Name
        };
    }

    public static Activity ToEntity(this CreateActivityDto dto)
    {
        return new Activity
        {
            Title = dto.Title,
            Type = dto.Type,
            Status = dto.Status,
            Date = dto.Date.ToUniversalTime(),
            DistanceInKm = dto.DistanceInKm,
            Location = new Location
            {
                Country = dto.Country,
                City = dto.City,
                Region = dto.Region
            },
            GpxFilePath = dto.GpxFilePath,
            KomootUrl = dto.KomootUrl
        };
    }

    public static void ApplyUpdate(this Activity activity, UpdateActivityDto updateActivityDto)
    {
        activity.Title = updateActivityDto.Title ?? activity.Title;
        activity.Type = updateActivityDto.Type ?? activity.Type;
        activity.Status = updateActivityDto.Status ?? activity.Status;
        activity.Duration = updateActivityDto.Duration ?? activity.Duration;
        activity.Date = updateActivityDto.Date?.ToUniversalTime() ?? activity.Date;
        activity.DistanceInKm = updateActivityDto.DistanceInKm ?? activity.DistanceInKm;
        activity.ElevationGain = updateActivityDto.ElevationGain ?? activity.ElevationGain;
        activity.ElevationLoss = updateActivityDto.ElevationLoss ?? activity.ElevationLoss;
        activity.GpxFilePath = updateActivityDto.GpxFilePath ?? activity.GpxFilePath;
        activity.KomootUrl = updateActivityDto.KomootUrl ?? activity.KomootUrl;
        activity.Location.Country = updateActivityDto.Country ?? activity.Location.Country;
        activity.Location.City = updateActivityDto.City ?? activity.Location.City;
        activity.Location.Region = updateActivityDto.Region ?? activity.Location.Region;
    }
}
