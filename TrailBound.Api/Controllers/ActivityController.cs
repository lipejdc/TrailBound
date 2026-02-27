using Microsoft.AspNetCore.Mvc;
using TrailBound.Application.Dtos.Activity;
using TrailBound.Application.Interfaces;
using TrailBound.KomootWrapper;

namespace TrailBound.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivityController(IActivityService activityService, IKomootClient komootClient) : ControllerBase
{
    private readonly IActivityService _activityService = activityService;
    private readonly IKomootClient _komootClient = komootClient;

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

    [HttpGet("tours")]
    public async Task<IActionResult> GetTours()
    {
        var authToken = "eyJ4NXQjUzI1NiI6InlIQ2xZdUdwWlNvU2c3dkhrT1k4YzYyR2NGdGxSeV9neEpIYkJfNFpjamsiLCJraWQiOiJqd3QtMjAyNTAyMTEiLCJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJydGkiOiJmYjlkYmVlMWZiOWJmMWFlNjJlMDkyOTVjMTAwNjlmYWVkZmU4ZTcwNjQwZTIwYmM5ZGE5OGU4NzU5NDA5NDc4IiwidXNlcl9uYW1lIjoiMTY0Mjc5MjI4ODM2OSIsInNjb3BlIjpbInVzZXIuKiJdLCJleHAiOjE3NzIyMTU5MjIsImlhdCI6MTc3MjIxNDEyMiwianRpIjoiZmM4MWE4M2YtOTdmNC00MGJhLWEwMGMtZjJjOWM0ZGE2OTA2IiwiY2xpZW50X2lkIjoia29tb290LXdlYiIsInVzZXJuYW1lIjoiMTY0Mjc5MjI4ODM2OSJ9.zzm8gAIoGf11N8JHflg3TVTfd7SPEEUyn_KvfvSvqPjlxhWiFI9kyuv9flAGkeQncV7Z6paiWsbvqLljXtYB78BQo1D5A55FWcvhielZlTu3InWU70lmBULo0hEGOtNsGyfTtleSTcx3PBmGDBYvyMlAYH9w40A1t6Rbz2roc79sSxn2H5dhyV8SB2Tj-y8a7QsegWjx19OdL25VA5WSR_0zAz3mbCqjj4sP_636scNmmjN5UgVCYfr-X-m69BnZzboFyNFwLvGRfA7ehMeGI515JeJz5jd32rBt6RtMn8HP99-OkGcGIHhhOJys0ehh_TsHcBzmEeP21ZU-VNmRUw";
        await _komootClient.GetToursAsync("1642792288369", authToken, CancellationToken.None);
        return Ok();
    }
}
