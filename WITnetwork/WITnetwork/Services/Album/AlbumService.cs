

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;
using WITnetwork.Dtos;
using WITnetwork.Models;

namespace WITnetwork.Services;

public class AlbumService(NetworkDBContext context, IMapper mapper, IPhotoService photoService) : IAlbumService
{
    public async Task<AlbumResponseDto> CreateAlbumAsync(CreateAlbumPayloadDto dto, long userId)
    {
        try
        {
            var profile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId) 
                ?? throw new Exception("profile not found");

            var album = new Album
            {
                Name = dto.Name,
                Theme = dto.Theme,
                Year = dto.Year,
                ProfileId = profile.Id,
            };

            context.Albums.Add(album);


            await context.SaveChangesAsync();

            var mappedAlbum = mapper.Map<AlbumResponseDto>(album);

            return mappedAlbum;


        } 
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public async Task<AlbumResponseDto> UpdateAlbumAsync(UpdateAlbumPayloadDto dto)
    {
        try
        {
            var album = await context.Albums.FirstOrDefaultAsync(a => a.Id == dto.Id) 
                ?? throw new Exception("album not found");

            album.Name = dto.Name ?? album.Name;
            album.Theme = dto.Theme ?? album.Theme;
            album.Year = dto.Year ?? album.Year;

            await context.SaveChangesAsync();

            var mappedAlbum = mapper.Map<AlbumResponseDto>(album);

            return mappedAlbum;
        } 
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public async Task<AlbumResponseDto> ToggleShownAlbumAsync(long id)
    {
        try
        {
            var album = await context.Albums.FirstOrDefaultAsync(a => a.Id == id) 
                ?? throw new Exception("album not found");

            album.IsShown = !album.IsShown;

            await context.SaveChangesAsync();

            var mappedAlbum = mapper.Map<AlbumResponseDto>(album);

            return mappedAlbum;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public async Task<AlbumResponseDto> DeleteAlbumAsync(long id)
    {
        try
        {
            var album = await context.Albums
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();
            
            await context.SaveChangesAsync();

            var mappedAlbum = mapper.Map<AlbumResponseDto>(album);

            return mappedAlbum;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public async Task<AlbumImageDto> AddAlbumImageAsync(long albumId, string image)
    {
        try
        {
            var album = await context.Albums.FirstOrDefaultAsync(a => a.Id == albumId) 
                ?? throw new Exception("album not found");

            var uploadedImageResult = await photoService.AddPhotoAsync(image);


            var newImage = new AlbumImage
            {
                AlbumId = album.Id,
                Image = uploadedImageResult.SecureUrl.AbsoluteUri,
                PublicId = uploadedImageResult.PublicId,
                IsShown = true
            };

            context.AlbumImages.Add(newImage);

            await context.SaveChangesAsync();

            var mappedImage = mapper.Map<AlbumImageDto>(newImage);

            return mappedImage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public async Task<AlbumImageDto> DeleteAlbumImageAsync(long albumId, long imageId)
    {
        try
        {
            var image = await context.AlbumImages.FirstOrDefaultAsync(i => i.Id == imageId)
                ?? throw new Exception("album image not found");

            var deletingImageResult = await photoService.DeletePhotoAsync(image.PublicId);

            if (deletingImageResult.Result == "ok")
            {
                context.AlbumImages.Remove(image);

                await context.SaveChangesAsync();

                var mappedImage = mapper.Map<AlbumImageDto>(image);

                return mappedImage;
            } else
            {
                throw new Exception($"Cloudinary delete failed: {deletingImageResult.Result}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public async Task<AlbumImageDto> ToggleShownAlbumAsync(long albumId, long imageId)
    {
        try
        {
            var image = await context.AlbumImages.FirstOrDefaultAsync(i => i.Id == imageId)
                ?? throw new Exception("album image not found");
            
            image.IsShown = !image.IsShown;

            await context.SaveChangesAsync();

            var mappedImage = mapper.Map<AlbumImageDto>(image);

            return mappedImage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public async Task<IEnumerable<AlbumResponseDto>> GetAllAlbumsAsync(long userId)
    {
        try
        {
            var profile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId) 
                ?? throw new Exception("profile not found");
            
            var albums = await context.Albums
                .Include(a => a.Images)
                .Where(a => a.ProfileId == profile.Id)
                .ToListAsync();

            var mappedAlbums = mapper.Map<IEnumerable<AlbumResponseDto>>(albums);

            return mappedAlbums;
            

        } 
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
}
