
using Microsoft.AspNetCore.Mvc;
using WITnetwork.Dtos;
using WITnetwork.Services;

namespace WITnetwork.Controller;

[ApiController]
[Route("api/user/[controller]")]
public class SettingsController(SettingsService settingsService) : ControllerBase
{
    [HttpPatch("update")]
    public async Task<IActionResult> UpdateUser([FromBody]UpdateUserDto dto)
    {
        var user = await settingsService.UpdateUser(dto);
        return Ok(new
        {
            Status = "success",
            data = user
        });
    }
}