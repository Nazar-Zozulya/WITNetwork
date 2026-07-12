




using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/user")]
[Authorize]
public class FriendshipController(IFriendshipService friendshipService) : ControllerBase
{
    private long CurrentUserId => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("friends")]
    public async Task<IActionResult> GetAllFriend()
    {
        try
        {
            return Ok(await friendshipService.GetFriendshipsAsync(CurrentUserId));
        }
        catch (Exception ex)
        {
            return BadRequest(new {status = "error", message = $"error getting friends: {ex}"});
        }
    }

    [HttpGet("requests")]
    public async Task<IActionResult> GetAllFriendRequests()
    {
        try
        {
            return Ok(await friendshipService.GetFriendRequestsAsync(CurrentUserId));
        }
        catch (Exception ex)
        {
            return BadRequest(new {status = "error", message = $"error getting requests: {ex}"});
        }
    }

    [HttpGet("recommendations")]
    public async Task<IActionResult> GetAllFriendRecommendations()
    {
        try
        {
            return Ok(await friendshipService.GetFriendRecommendationsAsync(CurrentUserId));
        }
        catch (Exception ex)
        {
            return BadRequest(new {status = "error", message = $"error getting recommendations: {ex}"});
        }
    }

    [HttpPost("send-request")]
    public async Task<IActionResult> SendFriendRequest([FromBody] RequestDto dto)
    {
        try
        {
            return Ok(await friendshipService.SendFriendRequestAsync(CurrentUserId, dto.UserId));
        } 
        catch (Exception ex)
        {
            return BadRequest(new {status = "error", message = $"error sending request: {ex}"});
        }
    }

    [HttpPatch("accept-request")]
    public async Task<IActionResult> AcceptFriendRequest([FromBody] RequestDto dto)
    {
        try
        {
            return Ok(await friendshipService.AcceptFriendRequestAsync(CurrentUserId, dto.UserId));
        } 
        catch (Exception ex)
        {
            return BadRequest(new {status = "error", message = $"error accepting request: {ex}"});
        }
    }

    [HttpDelete("delete-relationship")]
    public async Task<IActionResult> DeleteFriendRelationship([FromBody] RequestDto dto)
    {
        try
        {
            return Ok(await friendshipService.DeleteFriendRelationshipAsync(CurrentUserId, dto.UserId));
        }
        catch (Exception ex)
        {
            return BadRequest(new {status = "error", message = $"error deletting request: {ex}"});
        }
    }

    [HttpGet("which-relationship/{id}")]
    public async Task<IActionResult> WhichRelationship(long id)
    {
        try
        {
            return Ok(await friendshipService.WhichFriendshipAsync(CurrentUserId, id));
        } 
        catch (Exception ex)
        {
            return BadRequest(new {status = "error", message = $"error searching friendship status: {ex}"});    
        }
    }
}