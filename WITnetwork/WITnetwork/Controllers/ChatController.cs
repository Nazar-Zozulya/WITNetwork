

using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;
using WITnetwork.Dtos;

[ApiController]
[Route("api/user/[controller]")]
[Authorize]
public class ChatController (NetworkDBContext context) : ControllerBase
{
    [HttpPost("get-chat")]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatDto dto) {

        var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserIdStr == null)
        {
            return Unauthorized("Не вдалося определити користувача");
        }
        var currentUserId = Guid.Parse(currentUserIdStr);

        var allParticipantIds = dto.ParticipantsIds.ToList();

        allParticipantIds.Add(currentUserId);
        allParticipantIds = allParticipantIds.Distinct().ToList();

        var participants = await context.Users.Where(u => allParticipantIds
            .Contains(u.Id))
            .ToListAsync();

        if (participants.Count < 2)
        {
            return BadRequest("недостатньо користувачів для створення чатів");
        }

        var AdminUser = await context.Users.Where(u => u.Id == Guid.Parse(currentUserIdStr)).FirstOrDefaultAsync();

        if (AdminUser == null)
        {
            return BadRequest("user not found");
        }

        var newChat = new Chat
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            IsGroup = dto.IsGroup,
            Users = participants,
            AdminId = Guid.Parse(currentUserIdStr),
            Admin = AdminUser
        };

        context.Chats.Add(newChat);

        await context.SaveChangesAsync();

        return Ok(newChat);
    }
}