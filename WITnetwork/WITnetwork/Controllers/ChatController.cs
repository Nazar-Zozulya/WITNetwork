

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

    [HttpGet("chats/{id}")]
    public async Task<IActionResult> GetIndividualChats(long id)
    {
        try
        {
            var result = await chatService.GetIndividualChats(id);
            return Ok(new { status = "success", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = $"Error getting individual chats: {ex.Message}" });
        }
    }
}