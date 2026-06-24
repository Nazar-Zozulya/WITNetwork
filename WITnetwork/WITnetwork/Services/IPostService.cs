using WITnetwork.Models;

public interface IPostService
{
    Task<IEnumerable<PostResponseDto>> GetAllPostsAsync();

    Task<PostResponseDto> CreatePostAsync(CreatePostDto dto, Guid authorId );

    Task<PostResponseDto> GetPostByIdAsync(Guid postId);

    Task<string> DeletePostAsync(Guid postId, Guid userId);

}