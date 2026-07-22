using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WITnetwork.Dtos;
using WITnetwork.Services;

namespace NetworkAsp.Controllers;

[ApiController]
[Route("api/post")]
public class PostsController(IPostService postService) : ControllerBase
{
    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
    {        
        try
        {
            var authorIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (authorIdString == null)
            {
                return Unauthorized(new { status = "error", message = "Не вдалося визначити користувача" });
            }

            var authorId = long.Parse(authorIdString);

            // Уся логіка роботи з файлами та БД інкапсульована всередині сервісу
            var createdPost = await postService.CreatePostAsync(dto, authorId);

            return Ok(new {status = "success", data = createdPost});
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = "error", message = $"error creating post: {ex}" });
        }
    }


    [HttpGet("all")]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page,
        [FromQuery] int size)
    {
        try
        {
            var posts = await postService.GetAllPostsAsync(page, size);
            return Ok(new {status ="success", data = posts});      
        }
        catch (Exception ex)
        {
            return BadRequest(new {status = "error", message = $"error getting all posts: {ex}"});
        }
    }

    [HttpGet("all/{id}")]
    public async Task<IActionResult> GetAllPostsByUserId (
        [FromQuery] int page,
        [FromQuery] int size,
        long id
    )
    {
        try
        {
            // var authorIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // if (authorIdString == null)
            // {
            //     return BadRequest(new
            //     {
            //         status = "error",
            //         message = $"error getting all posts from user: user not found"
            //     });
            // }

            var posts = await postService.GetAllPostsByUserId(id, page, size);
            return Ok(new
            {
                status = "success",
                data = posts
            });
        } 
        catch (Exception ex)
        {
            return BadRequest(new
            {
                status = "error",
                message = $"error getting all posts from user: {ex}"
            });
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var post = await postService.GetPostByIdAsync(id);
        if (post == null) return NotFound(new { Message = "Пост не знайдено" });

        return Ok(post);
    }
    
    [HttpDelete("delete")]
    // [Authorize]
    public async Task<IActionResult> Delete([FromBody] DeletePostDto dto)
    {
        // var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        try
        {
            var result = await postService.DeletePostAsync(dto.Id, dto.UserId);
            if (result == null) return NotFound(new {status = "error", message = "deleting post error"});
            return Ok(new {status = "success", data = result});
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest(new {status = "error", message = $"Error deleting post: {ex.Message}"});
        }
    }
}
