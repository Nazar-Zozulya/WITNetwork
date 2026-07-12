

using WITnetwork.Dtos;

namespace WITnetwork.Services;

public  interface IUserService
{
    public Task<UserWithoutIncludes> CompleteProfileAsync(CompleteProfileDto dto, long id);

    public Task<UserResponseDto> GetUserAsync(long id);
}