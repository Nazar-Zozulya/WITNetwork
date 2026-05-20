


using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TagsController(ITagService tagService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var posts = tagService.GetAllTagsAsync();

        return Ok(posts);
    }

    public async Task<IActionResult> Create([FromBody] CreateTagDto dto)
    {
        var createTag = await tagService.CreateTagAsync(dto);
        return Ok(createTag);
    }
}