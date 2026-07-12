


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WITnetwork.Services;

[ApiController]
[Route("api/post")]
public class TagsController(ITagService tagService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var posts = await tagService.GetAllTagsAsync();

        return Ok(posts);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateTagDto dto)
    {
        var createTag = await tagService.CreateTagAsync(dto);
        return Ok(createTag);
    }
}