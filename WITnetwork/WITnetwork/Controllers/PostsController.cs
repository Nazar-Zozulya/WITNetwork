using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WITnetwork.Dtos;
using WITnetwork.Services;

namespace NetworkAsp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController(IPostService postService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var posts = await postService.GetAllPostsAsync();
        return Ok(posts);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromForm] CreatePostDto dto)
    {
        // Базова перевірка, чи дійшли файли взагалі
        if (dto.Images == null || !dto.Images.Any())
        {
            return BadRequest("Файли не дійшли до контролера!");
        }
        
        var authorIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (authorIdString == null)
        {
            return Unauthorized(new { Message = "Не вдалося визначити користувача" });
        }

        var authorId = Guid.Parse(authorIdString);

        // Уся логіка роботи з файлами та БД інкапсульована всередині сервісу
        var createdPost = await postService.CreatePostAsync(dto, authorId);

        return Ok(createdPost);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var post = await postService.GetPostByIdAsync(id);
        if (post == null) return NotFound(new { Message = "Пост не знайдено" });

        return Ok(post);
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        try
        {
            var result = await postService.DeletePostAsync(id, userId);
            if (result == null) return NotFound();
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex);
            return Forbid();
        }
    }
}
