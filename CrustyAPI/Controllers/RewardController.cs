
using CrustyAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrustyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RewardController : ControllerBase
{
    private readonly RewardService _rewardService;

    public RewardController(RewardService rewardService)
    {
        _rewardService = rewardService;
    }

    [HttpPost("orders/{orderId:int}/spin")]
    public async Task<IActionResult> Spin(
        int orderId,
        [FromBody] SpinRewardRequest request)
    {
        if (request.CustomerId <= 0 || orderId <= 0)
        {
            return BadRequest(new
            {
                message = "A valid customer ID and order ID are required."
            });
        }

        try
        {
            var result = await _rewardService.SpinAsync(
                request.CustomerId,
                orderId);

            return Ok(result);
        }
        catch (RewardException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

public sealed class SpinRewardRequest
{
    public int CustomerId { get; set; }
}
