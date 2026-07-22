using WITnetwork.Dtos;
using WITnetwork.Models;

public interface IPostService
{
    Task<IEnumerable<PostResponseDto>> GetAllPostsAsync(int page, int size);

    Task<PostResponseDto> CreatePostAsync(CreatePostDto dto, long authorId );

    Task<PostResponseDto> GetPostByIdAsync(long postId);


    Task<IEnumerable<PostResponseDto>> GetAllPostsByUserId(long userId,
    int page, int size
    );

    Task<PostResponseDto> DeletePostAsync(long postId, long userId);

}