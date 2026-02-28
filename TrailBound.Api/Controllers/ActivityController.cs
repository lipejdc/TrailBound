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
        var credentials = new Dictionary<string, string>
        {
            { "user_id", "1642792288369" },
            { "cookie", "cookie_id=4c2d17cc-2f9a-47b2-93ac-94ccec009468; g_state={\"i_l\":0,\"i_ll\":1772291967919,\"i_b\":\"V+LrznkmI5NtsXNVfsY/tGl7a2wU1uh2+TQ5Mu3bxow\",\"i_e\":{\"enable_itp_optimization\":0}}; __stripe_mid=354e39d0-9063-4add-83ab-14054574844e7ab70a; __stripe_sid=59ded507-c6f2-49d4-98be-a87758ad4870950852; kmt_sess=eyJsYW5nIjoiZW4iLCJtZXRyaWMiOnRydWUsInByb2ZpbGUiOnsidXNlcm5hbWUiOiIxNjQyNzkyMjg4MzY5IiwibG9jYWxlIjoiZW5fVVMiLCJtZXRyaWMiOnRydWV9fQ==; kmt_sess.sig=AJVQgnkFq1wUTuc2j-9hUiI5RnQ; showGoogleSsoFakeDoor=false; koa_re=1803828000; koa_rt=1642792288369%7C%2F1642792288369%2Fkomoot-web%2Fdac19fe3-8674-481a-a7d3-807df536672b%7C1803828000; koa_ae=1772293740; koa_at=1642792288369%7CeyJ4NXQjUzI1NiI6InlIQ2xZdUdwWlNvU2c3dkhrT1k4YzYyR2NGdGxSeV9neEpIYkJfNFpjamsiLCJraWQiOiJqd3QtMjAyNTAyMTEiLCJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJydGkiOiJjNDBlNzEwNDI5NmQxYTg0OTRkMTcyMTdjZTJlMzVlZmZjMWIwYWJhZTRiMzc1NzhjYjU5Nzk1YWNiOTljODY2IiwidXNlcl9uYW1lIjoiMTY0Mjc5MjI4ODM2OSIsInNjb3BlIjpbInVzZXIuKiJdLCJleHAiOjE3NzIyOTM4MDAsImlhdCI6MTc3MjI5MjAwMCwianRpIjoiYWNlMmE5YmYtNzA1MS00MWY4LWIzMTMtMGNlYmQxZTQzOWNjIiwiY2xpZW50X2lkIjoia29tb290LXdlYiIsInVzZXJuYW1lIjoiMTY0Mjc5MjI4ODM2OSJ9.rgsFBs7iKUrDecLV7_s-6GAf_FzRmGDf_ErWF632_h1F3Ihlho48MsA2rWR1sCnl_pEUh_U4MKtHZIneUaqXwEvd3JI_xpwV4U1pPG1Vi-tpYaliCZfOpZtCMQQ_oOpGXaYioyoCbGnaJywYwr_eWAdqquHQcMVseF2IQhU-_DXLhBN9BAJrz3CgAs90Ki1Gn4NvYD-Gs4tqJXq2KkIpoqcW7aHnQ2_nxch0vWBEo9vZoSnde80yqG5AbsB0R3I5ANoWM79lYtTlA4mxEOn9F2hqd6ovv1iruoueuRINOEoTQsPGy2UBoENPvdsbBzCKaEcWvRxeJnDVW84J9y4kCA%7C1772293740; fresh_signin=1642792288369" } };
            var authToken = "eyJ4NXQjUzI1NiI6InlIQ2xZdUdwWlNvU2c3dkhrT1k4YzYyR2NGdGxSeV9neEpIYkJfNFpjamsiLCJraWQiOiJqd3QtMjAyNTAyMTEiLCJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJydGkiOiJjNDBlNzEwNDI5NmQxYTg0OTRkMTcyMTdjZTJlMzVlZmZjMWIwYWJhZTRiMzc1NzhjYjU5Nzk1YWNiOTljODY2IiwidXNlcl9uYW1lIjoiMTY0Mjc5MjI4ODM2OSIsInNjb3BlIjpbInVzZXIuKiJdLCJleHAiOjE3NzIyOTk3OTUsImlhdCI6MTc3MjI5Nzk5NSwianRpIjoiZTNmNTYwY2EtMTNhNS00NzMwLThhOGMtMzFlMDg5YzkzNTM3IiwiY2xpZW50X2lkIjoia29tb290LXdlYiIsInVzZXJuYW1lIjoiMTY0Mjc5MjI4ODM2OSJ9.jaEzDLCHdjGBmteYt9xAriLNpIlqrmxJC_2q_Tq9PdMoxAIrzaT--sHvrelHEU2j3ARE3FlAR6t3WksGlVC5HlYIha7PIXMTM-reYUGDiPC6BOimFknUaV44-1cUFmmG2CG5bvzTo_jKsr-r2qIzdUG0L7PwsffVLxnWqn9T83lbWPiQbHktvzUkBPVcFhfua9s8hUXAir-KUkL_y5h9jXMeY1LnOQaLmtZuBIR78RfmxxYZvqiRrSwKyWb0O93hDFWKzfbODfiGPV5gPKP9Obud3MA25lJ7MqcRkf9F_3PYZP3NHLQ5gLCkNE6yqRYXewQ5erYxLNDhA52FDmJxQg";
            await _komootClient.GetToursAsync(
            "1642792288369", 
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
