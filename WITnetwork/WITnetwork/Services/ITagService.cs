



public interface ITagService
{
    Task<IEnumerable<TagResponseDto>> GetAllTagsAsync();

    Task<TagResponseDto> CreateTagAsync(CreateTagDto dto /** Guid authorId **/ );

}