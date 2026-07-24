

using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;
using WITnetwork.Dtos;
using WITnetwork.Models;
using WITnetwork.Services;

[ApiController]
[Route("api/chat")]
// [Authorize]
public class ChatController (NetworkDBContext context, IChatService chatService) : ControllerBase
{
    [HttpPost("get-chat")]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatDto dto) {
        try
        {
            var result = await chatService.GetChat(dto.UserId, dto.AnotherUserId);
            return Ok(new { status = "success", data = result });
        } 
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = $"Error getting chat: {ex.Message}" });
        }
    }

    [HttpGet("get-chat/{id}")]
    public async Task<IActionResult> CreateChat(long id) {
        try
        {
            var result = await chatService.GetChatById(id);
            return Ok(new { status = "success", data = result });
        } 
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = $"Error getting chat by id: {ex.Message}" });
        }
    }

    [HttpGet("chats/{id}")]
    public async Task<IActionResult> GetIndividualChats(long id, 
        [FromQuery] int page,
        [FromQuery] int size
    )
    {
        try
        {
            var result = await chatService.GetIndividualChats(id, page, size);
            return Ok(new { status = "success", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = $"Error getting individual chats: {ex.Message}" });
        }
    }

    [HttpGet("messages/{id}")]
    public async Task<IActionResult> GetMessagesFromChat(long id, [FromQuery] int page,[FromQuery] int size)
    {
        try
        {
            var result = await chatService.GetMessagesFromChat(id, page, size);
            return Ok(new { status = "success", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = $"Error getting messages from chat: {ex.Message}" });
        }
    }

    [HttpGet("groups/{id}")]
    public async Task<IActionResult> GetGroups(long id, 
        [FromQuery] int page,
        [FromQuery] int size
    )
    {
        try
        {
            var result = await chatService.GetGroups(id, page, size);
            return Ok(new { status = "success", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = $"Error getting groups: {ex.Message}" });
        }
    }

    [HttpPost("group/create")]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto dto)
    {
        try
        {
            var result = await chatService.CreateGroup(dto);
            return Ok(new { status = "success", data = result });
        } 
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = $"Error creating group: {ex.Message}" });
        }
    }
}