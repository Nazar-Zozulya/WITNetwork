

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WITnetwork.Dtos;
using WITnetwork.Services;

namespace WITnetwork.Controller;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController (IUserService userService) : ControllerBase
{
    private long CurrentUserId => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPatch("complete-profile")]
    public async Task<IActionResult> CompleteProfileAsync([FromBody] CompleteProfileDto dto)
    {
        try
        {
            var result = await userService.CompleteProfileAsync(dto,CurrentUserId);
            return Ok(new { status = "success", data = result });
        } 
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = $"Error compliting profile: {ex}" });
        }
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetUserAsync()
    {
        try
        {
            var result = await userService.GetUserAsync(CurrentUserId);
            return Ok(new { status = "success", data = result });
        } 
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = $"Error getting user: {ex}" });
        }
    }
}