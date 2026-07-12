
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WITnetwork.Dtos;
using WITnetwork.Services;

namespace WITnetwork.Controller;

[ApiController]
[Route("api/user")]
[Authorize]
public class SettingsController(ISettingsService settingsService) : ControllerBase
{
    [HttpPatch("update")]
    public async Task<IActionResult> UpdateUser([FromBody]UpdateUserDto dto)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
            var user = await settingsService.UpdateUser(dto, long.Parse(userId));
            return Ok(new
            {
                status = "success",
                data = user
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                status = "error",
                message = $"error updating user {ex}"
            });
        }
    }
}