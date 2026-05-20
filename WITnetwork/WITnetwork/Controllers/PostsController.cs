
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
    public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
    {
        var createPost = await postService.CreatePostAsync(dto);
        return Ok(createPost);
    }
}