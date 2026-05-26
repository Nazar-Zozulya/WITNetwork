using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WITnetwork.Models;

public interface IAuthService
{
    Task<IdentityResult?> Register(RegisterDto dto, UserManager<UserProfile> UserManager);

    Task<string?> Login(LoginDto dto, UserManager<UserProfile> UserManager );
}


