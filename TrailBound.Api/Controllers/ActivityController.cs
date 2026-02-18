using Microsoft.AspNetCore.Mvc;
using TrailBound.Application.Dtos;
using TrailBound.Application.Interfaces;

namespace TrailBound.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivityController(IActivityRepository activityRepository) : ControllerBase
{
    private readonly IActivityRepository _activityRepository = activityRepository;

    [HttpGet]
    public async Task<IActionResult> GetAllActivities()
    {
        var activities = await _activityRepository.GetActivitiesAsync();
        return Ok(activities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivityById(int id)
    {
        var activity = await _activityRepository.GetActivityByIdAsync(id);

        if (activity == null)
        {
            return NotFound();
        }

        return Ok(activity);
    }

    [HttpGet("year/{year}")]
    public async Task<IActionResult> GetActivitiesByYear(int year)
    {
        var activities = await _activityRepository.GetActivitiesByYearAsync(year);

        if (activities == null || !activities.Any())
        {
            return NotFound();
        }

        return Ok(activities);
    }

    [HttpGet("{year}/{month}")]
    public async Task<IActionResult> GetActivitiesByMonth(int year, int month)
    {
        var activities = await _activityRepository.GetActivitiesByMonthAsync(year, month);

        if (activities == null || !activities.Any())
        {
            return NotFound();
        }

        if (month < 1 || month > 12)
        {
            return BadRequest("Month must be between 1 and 12.");
        }

        return Ok(activities);
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityDto createActivityDto)
    {
        var activity = await _activityRepository.CreateActivityAsync(createActivityDto);

        return CreatedAtAction(nameof(GetActivityById), new { id = activity.Id }, activity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivity(int id, [FromBody] UpdateActivityDto updateActivityDto)
    {
        var updatedActivity = await _activityRepository.EditActivityAsync(id, updateActivityDto);

        if (updatedActivity == null)
        {
            return NotFound();
        }

        return Ok(updatedActivity);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(int id)
    {
        var activityDeleted = await _activityRepository.DeleteActivityAsync(id);

        if (!activityDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
