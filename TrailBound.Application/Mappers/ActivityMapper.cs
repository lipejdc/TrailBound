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
            Route = new ReadRouteDto
            {
                StartPoint = activity.Route.StartPoint,
                EndPoint = activity.Route.EndPoint,
                DistanceInKm = activity.Route.DistanceInKm,
                ElevationGain = activity.Route.ElevationGain,
                ElevationLoss = activity.Route.ElevationLoss
            },
            City = activity.Location.City,
            Region = activity.Location.Region,
            Country = activity.Location.Country,
            GpxFilePath = activity.GpxFilePath,
            KomootUrl = activity.KomootUrl,
            TripId = activity.TripId,
            TripName = activity.Trip?.Name
        };
    }

    public static Activity ToEntity(this CreateActivityDto createActivityDto)
    {
        return new Activity
        {
            Title = createActivityDto.Title,
            Type = createActivityDto.Type,
            Status = createActivityDto.Status,
            Date = createActivityDto.Date.ToUniversalTime(),
            Route = new Route
            {
                StartPoint = createActivityDto.Route.StartPoint,
                EndPoint = createActivityDto.Route.EndPoint,
                DistanceInKm = createActivityDto.Route.DistanceInKm,
                ElevationGain = createActivityDto.Route.ElevationGain,
                ElevationLoss = createActivityDto.Route.ElevationLoss
            },
            Location = new Location
            {
                Country = createActivityDto.Country,
                City = createActivityDto.City,
                Region = createActivityDto.Region
            },
            GpxFilePath = createActivityDto.GpxFilePath,
            KomootUrl = createActivityDto.KomootUrl
        };
    }

    public static void ApplyUpdate(this Activity activity, UpdateActivityDto updateActivityDto)
    {
        activity.Title = updateActivityDto.Title ?? activity.Title;
        activity.Type = updateActivityDto.Type ?? activity.Type;
        activity.Status = updateActivityDto.Status ?? activity.Status;
        activity.Duration = updateActivityDto.Duration ?? activity.Duration;
        activity.Date = updateActivityDto.Date?.ToUniversalTime() ?? activity.Date;
        activity.GpxFilePath = updateActivityDto.GpxFilePath ?? activity.GpxFilePath;
        activity.KomootUrl = updateActivityDto.KomootUrl ?? activity.KomootUrl;
        activity.Location.Country = updateActivityDto.Country ?? activity.Location.Country;
        activity.Location.City = updateActivityDto.City ?? activity.Location.City;
        activity.Location.Region = updateActivityDto.Region ?? activity.Location.Region;

        if (updateActivityDto.Route != null)
        {
            activity.Route.StartPoint = updateActivityDto.Route.StartPoint ?? activity.Route.StartPoint;
            activity.Route.EndPoint = updateActivityDto.Route.EndPoint ?? activity.Route.EndPoint;
            activity.Route.DistanceInKm = updateActivityDto.Route.DistanceInKm ?? activity.Route.DistanceInKm;
            activity.Route.ElevationGain = updateActivityDto.Route.ElevationGain ?? activity.Route.ElevationGain;
            activity.Route.ElevationLoss = updateActivityDto.Route.ElevationLoss ?? activity.Route.ElevationLoss;
        }
    }
}
