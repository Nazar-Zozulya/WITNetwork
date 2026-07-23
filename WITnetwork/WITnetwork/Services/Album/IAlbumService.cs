

using WITnetwork.Dtos;

namespace WITnetwork.Services;

public interface IAlbumService
{
    Task<AlbumResponseDto> CreateAlbumAsync(CreateAlbumPayloadDto dto, long userId);

    Task<AlbumResponseDto> UpdateAlbumAsync(UpdateAlbumPayloadDto dto);

    Task<AlbumResponseDto> ToggleShownAlbumAsync(long id);

    Task<AlbumResponseDto> DeleteAlbumAsync(long id);

    Task<AlbumImageDto> AddAlbumImageAsync(long albumId, string image);

    Task<AlbumImageDto> DeleteAlbumImageAsync(long albumId, long imageId);

    Task<AlbumImageDto> ToggleShownAlbumAsync(long albumId, long imageId);

    Task<IEnumerable<AlbumResponseDto>> GetAllAlbumsAsync(long userId, int page, int size);
}