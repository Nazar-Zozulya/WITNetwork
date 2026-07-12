

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;
using WITnetwork.Dtos;
using WITnetwork.Models;

namespace WITnetwork.Services;

public class SettingsService(IMapper mapper, NetworkDBContext context) : ISettingsService {
    public async Task<UserProfile> UpdateUser(UpdateUserDto dto, long id)
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

            if  (dto.Avatar != null || dto.BirthDate != null)
            {
                var profile = await context.Profiles
                    .FirstOrDefaultAsync(p => p.UserId == dto.Id);

                if (profile != null)
                {
                    user.Profile.Avatar = dto.Avatar ?? profile.Avatar;
                    user.Profile.BirthDate = dto.BirthDate ?? profile.BirthDate;
                }
            }

            await context.SaveChangesAsync();

            return user;

        } 
        catch (Exception ex)
        {
            throw new Exception($"{ex}");
        }
    }
}
