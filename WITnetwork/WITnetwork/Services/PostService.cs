



using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;
using WITnetwork.Models;
using WITnetwork.Services;

public class PostService(NetworkDBContext context, IMapper mapper, PhotoService photoService) : IPostService
{
    public async Task<IEnumerable<PostResponseDto>> GetAllPostsAsync()
    {
        var posts = await context.Posts
            .ToListAsync();

        return mapper.Map<IEnumerable<PostResponseDto>>(posts);
    }

    public async Task<PostResponseDto> CreatePostAsync(CreatePostDto dto, Guid authorId)
    {
        var post = mapper.Map<Post>(dto);

        post.AuthorId = authorId;

        context.Posts.Add(post);

        post.Images = new List<PostImage>();

        await context.SaveChangesAsync();

        foreach (var file in dto.Images)
        {
            var uploadResult = await photoService.AddPhotoAsync(file);

            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            var postImage = new PostImage
            {
                PostId = post.Id,
                Url = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId
            };

            context.PostImages.Add(postImage);
        }

        await context.SaveChangesAsync();

        var createdPost = await context.Posts
            .Include(p => p.Images)
            .FirstAsync(p => p.Id == post.Id);

        return mapper.Map<PostResponseDto>(post);

    }

    public async Task<PostResponseDto> GetPostByIdAsync(Guid postId)
    {
        var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == postId);

        return mapper.Map<PostResponseDto>(post);
    }

    public async Task<string> DeletePostAsync(Guid postId, Guid userId)
    {
        var post = await context.Posts.Where(p => p.Id == postId)
            .ExecuteDeleteAsync();

        // if (post == null) return "post not found";

        // if (post.AuthorId )
        // {
        //     return "its not your post";
        // }


        return "post deleted";
    }

}