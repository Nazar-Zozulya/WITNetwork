



using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;
using WITnetwork.Models;

public class PostService(NetworkDBContext context, IMapper mapper) : IPostService
{
    public async Task<IEnumerable<PostResponseDto>> GetAllPostsAsync()
    {
        var posts = await context.Posts
            .ToListAsync();

        return mapper.Map<IEnumerable<PostResponseDto>>(posts);
    }

    public async Task<PostResponseDto> CreatePostAsync(CreatePostDto dto)
    {
        var post = mapper.Map<Post>(dto);

        context.Posts.Add(post);
        await context.SaveChangesAsync();

        return mapper.Map<PostResponseDto>(post);

    }

}