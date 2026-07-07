


using WITnetwork.Dtos;
using WITnetwork.Models;

namespace WITnetwork.Services;

public interface ISettingsService {
    public Task<UserProfile> UpdateUser(UpdateUserDto dto);
}
