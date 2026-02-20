using Microsoft.AspNetCore.Mvc;
using TrailBound.Application.Dtos.Activity;
using TrailBound.Application.Interfaces;

namespace TrailBound.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivityController(IActivityService activityService) : ControllerBase
{
    private readonly IActivityService _activityService = activityService;

    [HttpGet]
    public async Task<IActionResult> GetAllActivities()
    {
        var activities = await _activityService.GetActivitiesAsync();
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
}
