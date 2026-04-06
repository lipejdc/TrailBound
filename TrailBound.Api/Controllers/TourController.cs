using Microsoft.AspNetCore.Mvc;
using TrailBound.KomootWrapper.Enums;
using TrailBound.KomootWrapper.Interfaces;
using TrailBound.Domain.Pagination;

namespace TrailBound.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TourController(IKomootClient komootClient) : ControllerBase
{
    private readonly IKomootClient _komootClient = komootClient;

    [HttpGet("tours")]
    public async Task<IActionResult> GetTours()
    {
        var credentials = new Dictionary<string, string>
    {
        { "user_id", "1642792288369" },
        { "cookie", "cookie_id=4c2d17cc-2f9a-47b2-93ac-94ccec009468; g_state={\"i_l\":0,\"i_ll\":1772291967919,\"i_b\":\"V+LrznkmI5NtsXNVfsY/tGl7a2wU1uh2+TQ5Mu3bxow\",\"i_e\":{\"enable_itp_optimization\":0}}; __stripe_mid=354e39d0-9063-4add-83ab-14054574844e7ab70a; __stripe_sid=59ded507-c6f2-49d4-98be-a87758ad4870950852; kmt_sess=eyJsYW5nIjoiZW4iLCJtZXRyaWMiOnRydWUsInByb2ZpbGUiOnsidXNlcm5hbWUiOiIxNjQyNzkyMjg4MzY5IiwibG9jYWxlIjoiZW5fVVMiLCJtZXRyaWMiOnRydWV9fQ==; kmt_sess.sig=AJVQgnkFq1wUTuc2j-9hUiI5RnQ; showGoogleSsoFakeDoor=false; koa_re=1803828000; koa_rt=1642792288369%7C%2F1642792288369%2Fkomoot-web%2Fdac19fe3-8674-481a-a7d3-807df536672b%7C1803828000; koa_ae=1772293740; koa_at=1642792288369%7CeyJ4NXQjUzI1NiI6InlIQ2xZdUdwWlNvU2c3dkhrT1k4YzYyR2NGdGxSeV9neEpIYkJfNFpjamsiLCJraWQiOiJqd3QtMjAyNTAyMTEiLCJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJydGkiOiJjNDBlNzEwNDI5NmQxYTg0OTRkMTcyMTdjZTJlMzVlZmZjMWIwYWJhZTRiMzc1NzhjYjU5Nzk1YWNiOTljODY2IiwidXNlcl9uYW1lIjoiMTY0Mjc5MjI4ODM2OSIsInNjb3BlIjpbInVzZXIuKiJdLCJleHAiOjE3NzIyOTM4MDAsImlhdCI6MTc3MjI5MjAwMCwianRpIjoiYWNlMmE5YmYtNzA1MS00MWY4LWIzMTMtMGNlYmQxZTQzOWNjIiwiY2xpZW50X2lkIjoia29tb290LXdlYiIsInVzZXJuYW1lIjoiMTY0Mjc5MjI4ODM2OSJ9.rgsFBs7iKUrDecLV7_s-6GAf_FzRmGDf_ErWF632_h1F3Ihlho48MsA2rWR1sCnl_pEUh_U4MKtHZIneUaqXwEvd3JI_xpwV4U1pPG1Vi-tpYaliCZfOpZtCMQQ_oOpGXaYioyoCbGnaJywYwr_eWAdqquHQcMVseF2IQhU-_DXLhBN9BAJrz3CgAs90Ki1Gn4NvYD-Gs4tqJXq2KkIpoqcW7aHnQ2_nxch0vWBEo9vZoSnde80yqG5AbsB0R3I5ANoWM79lYtTlA4mxEOn9F2hqd6ovv1iruoueuRINOEoTQsPGy2UBoENPvdsbBzCKaEcWvRxeJnDVW84J9y4kCA%7C1772293740; fresh_signin=1642792288369" } };
        var authToken = "eyJ4NXQjUzI1NiI6InlIQ2xZdUdwWlNvU2c3dkhrT1k4YzYyR2NGdGxSeV9neEpIYkJfNFpjamsiLCJraWQiOiJqd3QtMjAyNTAyMTEiLCJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJydGkiOiIxODJhYjllMGM3NzZhYzRhYTkyMWMzODRiN2I2NDI5YTY0OWE2MzU0Y2Q1M2NmYzZkN2I2YjIxN2MzNTM3NmJkIiwidXNlcl9uYW1lIjoiMTY0Mjc5MjI4ODM2OSIsInNjb3BlIjpbInVzZXIuKiJdLCJleHAiOjE3NzU0OTA4MjgsImlhdCI6MTc3NTQ4OTAyOCwianRpIjoiMjdkMzVkNjAtMzQ1OC00YjEyLWJlNTMtZTU5YmI0ZWMzMDc5IiwiY2xpZW50X2lkIjoia29tb290LXdlYiIsInVzZXJuYW1lIjoiMTY0Mjc5MjI4ODM2OSJ9.eQYPyPO22xBFouowMMfKswvrPFYCIUqUDI0wgQNEmizXSVxr3GYnbWcJcYx3keycB-5qFVAyDlNWK0zIUh8gqw-69LlMnsGBKcwOf-Bsfq40oiodmCGxnuXB_sb2tLtg3VUQhJHDsp4ERTb2LbvvNK4Om38OMG2vD0SyJuevrGS7r--wufmjmgEZgfADRH9EjC_Zn16YqPAay0ehzON8G5Ay59kYy6DzuFLENv7Z7BSZuZUH-0UBpNA7mOhckpC4c0bn16Umhn54kpvMndKSl7rMSFdxyYqIQy7YQbRMiZVP6hiPubW6cL59gNCJQlROkKGTUdCfHtDr_5MNX7K5eQ";

        var tours = await _komootClient.GetToursAsync(
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
        var paginatedTours = new PaginatedResult<KomootTour>(tours.Embedded.Tours, 1, 5, 10);

        return Ok(paginatedTours);
    }
}
