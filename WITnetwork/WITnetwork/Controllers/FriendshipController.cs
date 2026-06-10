




using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/user/[controller]")]
[Authorize]
public class FriendshipController(IFriendshipService friendshipService) : ControllerBase
{
    private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("friends")]
    public async Task<IActionResult> GetAllFriend() => Ok(await friendshipService.GetFriendshipsAsync(CurrentUserId));

    [HttpGet("requests")]
    public async Task<IActionResult> GetAllFriendRequests() => Ok(await friendshipService.GetFriendRequestsAsync(CurrentUserId));

    [HttpGet("recommendations")]
    public async Task<IActionResult> GetAllFriendRecommendations() => Ok(await friendshipService.GetFriendRecommendationsAsync(CurrentUserId));

    [HttpPost("send-request")]
    public async Task<IActionResult> SendFriendRequest([FromBody] RequestDto dto) => Ok(await friendshipService.SendFriendRequestAsync(CurrentUserId, dto.UserId));

    [HttpPatch("accept-request")]
    public async Task<IActionResult> AcceptFriendRequest([FromBody] RequestDto dto) => Ok(await friendshipService.AcceptFriendRequestAsync(CurrentUserId, dto.UserId));

    [HttpDelete("delete-relationship")]
    public async Task<IActionResult> DeleteFriendRelationship([FromBody] RequestDto dto) => Ok(await friendshipService.DeleteFriendRelationshipAsync(CurrentUserId, dto.UserId));
}