



using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;
using WITnetwork.Dtos;
using WITnetwork.Models;
using WITnetwork.Services;

public class PostService(NetworkDBContext context, IMapper mapper, IPhotoService photoService) : IPostService
{
    public async Task<IEnumerable<PostResponseDto>> GetAllPostsAsync(int page, int size)
    {
        try
        {
            var posts = await context.Posts
                .Include(p => p.Author)
                .Include(p => p.Images)
                .Include(p => p.Tags)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return mapper.Map<IEnumerable<PostResponseDto>>(posts);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public async Task<PostResponseDto> CreatePostAsync(CreatePostDto dto, long authorId)
    {
        try
        {        
            var post = mapper.Map<Post>(dto);

            post.AuthorId = authorId;

            context.Posts.Add(post);

            post.Images = new List<PostImage>();
            post.Tags = new List<Tag>();

            await context.SaveChangesAsync();

            foreach (var image in dto.Images )
            {
                var uploadResult = await photoService.AddPhotoAsync(image);

                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }

                var postImage = new PostImage
                {
                    PostId = post.Id,
                    OriginalImage = uploadResult.SecureUrl.AbsoluteUri,
                    PublicId = uploadResult.PublicId
                };

                context.PostImages.Add(postImage);
            }

            await context.SaveChangesAsync();

            foreach (var tag in dto.Tags)
            {
                var tagName = tag;

                var existingTag = await context.Tags
                    .FirstOrDefaultAsync(t => t.Name == tagName);

                if (existingTag != null)
                {
                    post.Tags.Add(existingTag);
                }
                else
                {
                    var newTag = new Tag { Name = tagName };
                    post.Tags.Add(newTag);
                }

            }

            await context.SaveChangesAsync();

            var createdPost = await context.Posts
                .Include(p => p.Images)
                .Include(p => p.Tags)
                .Include(p => p.Author)
                .FirstAsync(p => p.Id == post.Id);

            return mapper.Map<PostResponseDto>(post);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public async Task<PostResponseDto> GetPostByIdAsync(long postId)
    {
        var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == postId);

        return mapper.Map<PostResponseDto>(post);
    }

    public async Task<PostResponseDto> DeletePostAsync(long postId, long userId)
    {
        var post = await context.Posts
            .FirstOrDefaultAsync(p => p.Id == postId);

        if (post == null)
            throw new Exception("post not found");

        if (post.AuthorId != userId)
            throw new Exception("It's not your post");

        var mappedDeletedPost = mapper.Map<PostResponseDto>(post);
        
        context.Posts.Remove(post);

        await context.SaveChangesAsync();

        return mappedDeletedPost;
    }

    public async Task<IEnumerable<PostResponseDto>> GetAllPostsByUserId 
    (
        long userId,
        int page, 
        int size
    )
    {
        try
        {
            var posts = await context.Posts
                .Include(p => p.Author)
                .Include(p => p.Images)
                .Include(p => p.Tags)
                .Where(p => p.AuthorId == userId)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return mapper.Map<IEnumerable<PostResponseDto>>(posts);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

}