using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WITnetwork.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController (IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto, UserManager<UserProfile> UserManager)
    {
        // создаем соль для хеширования
        // byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); 
        // хешируем пароль
        // string HashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //     password: dto.Password!,
        //     salt: salt,
        //     prf: KeyDerivationPrf.HMACSHA256,
        //     iterationCount: 100000,
        //     numBytesRequested: 256 / 8));
        // Если что способ хеширования взял от сюда
        // https://learn.microsoft.com/ru-ru/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-10.0
        var createdUser = authService.Register(dto, UserManager);
        return Ok(createdUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto, UserManager<UserProfile> UserManager)
    {
        var loginedUser = authService.Login(dto, UserManager);
        return Ok(loginedUser);
    }
}