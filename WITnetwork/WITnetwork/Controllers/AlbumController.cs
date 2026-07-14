using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WITnetwork.Dtos;
using WITnetwork.Services;

namespace WITnetwork.Controller;

[ApiController]
[Route("api/user/albums")]
[Authorize]
public class AlbumController(IAlbumService albumService) : ControllerBase
{
    private long CurrentUserId => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("create")]
    public async Task<IActionResult> CreateAlbum([FromBody] CreateAlbumPayloadDto dto)
    {
        try
        {
            var result = await albumService.CreateAlbumAsync(dto, CurrentUserId);
            return Ok(new { status = "success", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = ex.Message });
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAlbum([FromBody] UpdateAlbumPayloadDto dto)
    {
        try
        {
            var result = await albumService.UpdateAlbumAsync(dto);
            return Ok(new { status = "success", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = ex.Message });
        }
    }

    [HttpPatch("switch-shown")]
    public async Task<IActionResult> ToggleSwitchShownAlbum([FromBody] AlbumPayloadDto dto)
    {
        try
        {
            var result = await albumService.ToggleShownAlbumAsync(dto.AlbumId);
            return Ok(new { status = "success", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = ex.Message });
        }
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAlbum([FromBody] AlbumPayloadDto dto)
    {
        try
        {
            var result = await albumService.DeleteAlbumAsync(dto.AlbumId);
            return Ok(new { status = "success", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = ex.Message });
        }
    }

    [HttpPost("image/add")]
    public async Task<IActionResult> AddAlbumImage([FromBody] CreateAlbumImagePayloadDto dto)
    {
        try
        {
            var result = await albumService.AddAlbumImageAsync(dto.AlbumId, dto.Image);
            return Ok(new { status = "success", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = ex.Message });
        }
    }

    [HttpDelete("image/delete")]
    public async Task<IActionResult> DeleteAlbumImage([FromBody] AlbumImagePayloadDto dto)
    {
        try
        {
            var result = await albumService.DeleteAlbumImageAsync(dto.AlbumId, dto.ImageId);
            return Ok(new { status = "success", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = ex.Message });
        }
    }

    [HttpPatch("image/switch-shown")]
    public async Task<IActionResult> ToggleSwitchShownAlbumImage([FromBody] AlbumImagePayloadDto dto)
    {
        try
        {
            var result = await albumService.ToggleShownAlbumAsync(dto.AlbumId, dto.ImageId);
            return Ok(new { status = "success", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = ex.Message });
        }
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllAlbums()
    {
        try
        {
            var result = await albumService.GetAllAlbumsAsync(CurrentUserId);
            return Ok(new { status = "success", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = ex.Message });
        }
    }
}