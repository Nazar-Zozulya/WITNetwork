

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;
using WITnetwork.Dtos;
using WITnetwork.Models;

namespace WITnetwork.Services;

public class SettingsService(IMapper mapper, NetworkDBContext context, IPhotoService photoService) : ISettingsService {
    public async Task<UserResponseDto> UpdateUser(UpdateUserDto dto, long id)
    {
        try
        {
            
            var user = await context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("user fot found");
            }


                user.FirstName = dto.FirstName ?? user.FirstName;
                user.LastName = dto.LastName ?? user.LastName;


            if (dto.Email != null)
            {
                var findEmail = await context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
                if (findEmail == null)
                {
                    user.Email = dto.Email;
                }
            }

            if  (dto.BirthDate != null)
            {
                var profile = await context.Profiles
                    .FirstOrDefaultAsync(p => p.UserId == dto.Id);

                if (profile != null)
                {
                    // user.Profile.Avatar = dto.Avatar ?? profile.Avatar;
                    user.Profile.BirthDate = dto.BirthDate ?? profile.BirthDate;
                }
            }

            System.Console.WriteLine(1111111111111111111);
            if (dto.Avatar != null)
            {
                System.Console.WriteLine(22222222);
                var profile = await context.Profiles
                    .Include(p => p.Albums.Where(a => a.IsMyPhotoAlbum == true))
                    .FirstOrDefaultAsync(p => p.UserId == id);
                System.Console.WriteLine(3333333333);

                if (profile != null)
                {
                    System.Console.WriteLine(44444444);
                    var myAlbum = profile.Albums.FirstOrDefault();
                    System.Console.WriteLine(55555555);
                    var currentMyAlbum = await context.Albums.Include(a => a.Images).FirstOrDefaultAsync(a => a.Id == myAlbum.Id);
                    System.Console.WriteLine(6666666666);

                    var uploadedImage = await photoService.AddPhotoAsync(dto.Avatar);
                    System.Console.WriteLine(77777777777);

                    var newImage = new AlbumImage
                    {
                        IsShown = true,
                        Image = uploadedImage.SecureUrl.AbsoluteUri,
                        PublicId = uploadedImage.PublicId,
                        AlbumId = myAlbum.Id
                    };
                    System.Console.WriteLine(888888888888);
                    
                    currentMyAlbum.Images.Add(newImage);
                    System.Console.WriteLine(99999999999999);

                    profile.Avatar = newImage;
                    System.Console.WriteLine(000000000000000);


                    await context.SaveChangesAsync();
                }
            }

            if (dto.Username != null)
            {
                var userFromUsername = await context.Users.FirstOrDefaultAsync(u => u.UserName == dto.Username);

                if (userFromUsername != null)
                {
                    throw new Exception("username already taken");
                }

                user.UserName = dto.Username;
            }

            await context.SaveChangesAsync();

            var mappedUser = mapper.Map<UserResponseDto>(user);

            return mappedUser;

        } 
        catch (Exception ex)
        {
            throw new Exception($"{ex}");
        }
    }
}
