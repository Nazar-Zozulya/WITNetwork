


using WITnetwork.Dtos;
using WITnetwork.Models;

namespace WITnetwork.Services;

public interface ISettingsService {
    public Task<UserResponseDto> UpdateUser(UpdateUserDto dto, long id);
}
