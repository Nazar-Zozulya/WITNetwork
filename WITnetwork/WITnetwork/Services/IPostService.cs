public interface IPostService
{
    Task<IEnumerable<PostResponseDto>> GetAllPostsAsync();

    Task<PostResponseDto> CreatePostAsync(CreatePostDto dto /** Guid authorId **/ );
}