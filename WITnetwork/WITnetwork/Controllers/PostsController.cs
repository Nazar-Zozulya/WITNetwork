
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PostController(IPostService postService) : ControllerBase
{
    [HttpGet]
    public async Task <IActionResult> GetAll()
    {
        var posts = postService.GetAllPostsAsync();

        return Ok(posts);
    }

    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
    {

        var authorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (authorId == null)
        {
            return BadRequest("unauthorized");
        }

        var createPost = await postService.CreatePostAsync(dto, authorId);
        return Ok(createPost);
    }
}