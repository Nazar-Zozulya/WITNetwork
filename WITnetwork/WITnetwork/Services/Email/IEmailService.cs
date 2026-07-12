

using WITnetwork.Dtos;

namespace WITnetwork.Services;

public interface IEmailService
{
    public Task<bool> SendVerificationEmailAsync(SendVerificationEmailDto dto);
}
