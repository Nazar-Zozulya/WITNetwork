

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;
using WITnetwork.Dtos;

namespace WITnetwork.Services;

public class UserService(IMapper mapper, NetworkDBContext context) : IUserService
{
    public async Task<UserWithoutIncludes> CompleteProfileAsync(CompleteProfileDto dto, long id)
    {
        try
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("user not found");
            }
            
            user.FirstName = dto.FirstName ?? user.FirstName;
            user.LastName = dto.LastName ?? user.LastName;
            user.UserName = dto.Username ?? user.UserName;

            await context.SaveChangesAsync();

            var userDto = mapper.Map<UserWithoutIncludes>(user);

            return userDto;
        } 
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public async Task<UserResponseDto> GetUserAsync(long id)
    {
        try
        {
            var user = await context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("user not found");
            }

            var userDto = mapper.Map<UserResponseDto>(user);

            return userDto;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
}