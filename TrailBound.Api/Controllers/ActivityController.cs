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

    [HttpGet("activity/{id}")]
    public async Task<IActionResult> GetActivityById(int id)
    {
        var activity = await _activityRepository.GetActivityByIdAsync(id);
        return Ok(activity);
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityDto createActivityDto)
    {
        var activity = await _activityRepository.CreateActivityAsync(createActivityDto);
        return CreatedAtAction(nameof(GetActivityById), new { id = activity.Id }, activity);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteActivity(int id)
    {
        var activity = await _activityRepository.DeleteActivityAsync(id);

        if (!activity)
        {
            return NotFound();
        }

        return NoContent();
    }
}
