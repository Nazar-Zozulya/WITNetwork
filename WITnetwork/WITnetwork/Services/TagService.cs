





using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;
using WITnetwork.Models;

public class TagService(NetworkDBContext context, IMapper mapper) : ITagService
{
    public async Task<IEnumerable<TagResponseDto>> GetAllTagsAsync()
    {
        var tags = await context.Tags
            .ToListAsync();

        
        return mapper.Map<IEnumerable<TagResponseDto>>(tags);
    }

    public async Task<TagResponseDto> CreateTagAsync(CreateTagDto dto)
    {
        var tag = mapper.Map<Tag>(dto);

        context.Tags.Add(tag);
        await context.SaveChangesAsync();

        return mapper.Map<TagResponseDto>(tag);
    }

}