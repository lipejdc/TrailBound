using Microsoft.AspNetCore.Mvc;
using TrailBound.Application.Dtos.Activity;
using TrailBound.Application.Dtos.Pagination;
using TrailBound.Application.Interfaces;
using TrailBound.Domain.Entities;
using TrailBound.KomootWrapper;

namespace TrailBound.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivityController(
    IActivityService activityService,
    IActivityRepository activityRepository,
    IKomootClient komootClient,
    IConfiguration configuration) : ControllerBase
{
    private readonly IActivityService _activityService = activityService;
    private readonly IKomootClient _komootClient = komootClient;
    private readonly IConfiguration _configuration = configuration;
    private readonly IActivityRepository _activityRepository = activityRepository;

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<Activity>>> GetAllActivities(
        int pageNumber, 
        int pageSize, 
        string? searchFilter,
        string? activityTypeFilter,
        DateTimeOffset? startDateFilter,
        DateTimeOffset? endDateFilter,
        TimeSpan minDurationFilter,
        TimeSpan maxDurationFilter,
        int minDistanceFilter,
        int maxDistanceFilter
        )
    {
        var paginationFilter = new ActivityPaginationFilter
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Title = searchFilter,
            ActivityType = activityTypeFilter,
            StartDate = startDateFilter ?? DateTimeOffset.MinValue,
            EndDate = endDateFilter ?? DateTimeOffset.MaxValue,
            MinimalDuration = minDurationFilter,
            MaximalDuration = maxDurationFilter,
            MinimalDistanceInMeters = minDistanceFilter,
            MaximalDistanceInMeters = maxDistanceFilter
        };

        var activities = await _activityRepository.GetAllAsync(paginationFilter);
        return Ok(activities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivityById(int id)
    {
        var activity = await _activityService.GetActivityByIdAsync(id);

        if (activity == null)
        {
            return NotFound();
        }

        return Ok(activity);
    }

    [HttpGet("year/{year}")]
    public async Task<IActionResult> GetActivitiesByYear(int year)
    {
        var activities = await _activityService.GetActivitiesByYearAsync(year);

        if (activities == null || !activities.Any())
        {
            return NotFound();
        }

        return Ok(activities);
    }

    [HttpGet("{year}/{month}")]
    public async Task<IActionResult> GetActivitiesByMonth(int year, int month)
    {
        var activities = await _activityService.GetActivitiesByMonthAsync(year, month);

        if (!activities.Any())
        {
            return NotFound();
        }

        return Ok(activities);
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityDto createActivityDto)
    {
        var activity = await _activityService.CreateActivityAsync(createActivityDto);

        return CreatedAtAction(nameof(GetActivityById), new { id = activity.Id }, activity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivity(int id, [FromBody] UpdateActivityDto updateActivityDto)
    {
        var updatedActivity = await _activityService.UpdateActivityAsync(id, updateActivityDto);

        if (updatedActivity == null)
        {
            return NotFound();
        }

        return Ok(updatedActivity);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(int id)
    {
        var activityDeleted = await _activityService.DeleteActivityAsync(id);

        if (!activityDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("tours")]
    public async Task<IActionResult> GetTours()
    {
        var credentials = new Dictionary<string, string>
        {
            { "user_id", _configuration["Komoot:UserId"] },
            { "cookie", _configuration["Komoot:Cookie"] }
        };
        
        var authToken = _configuration["Komoot:AuthToken"];
        
        await _komootClient.GetToursAsync(
            credentials["user_id"],
            authToken, 
            TourType.TourRecorded, 
            "date", 
            "desc", 
            TourStatus.Public,
            0,
            1000,
            CancellationToken.None
            );
        return Ok();
    }
}
