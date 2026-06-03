using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WITnetwork.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController (UserManager<UserProfile> UserManager) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
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
        
        var NewUser = new UserProfile{
            Email = dto.Email,
            UserName = "hh"
            // PasswordHash = dto.Password,
        };

        var result = await UserManager.CreateAsync(NewUser, dto.Password);
        // return result;

        // var createdUser = authService.Register(dto, UserManager);
        return Ok(result);
    }

     [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {

        var user = await UserManager.FindByEmailAsync(dto.Email);
        
        if (user == null || !await UserManager.CheckPasswordAsync(user, dto.Password))
        {
            return Unauthorized(new { Message = "Неверный email или пароль" });
        }

        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        // Этот ключ должен в точности совпадать с тем, что в Program.cs
        var key = Encoding.UTF8.GetBytes("SuperSecretKeyForDevelopment12345!SuperSecretKey");
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                new Claim(ClaimTypes.Email, user.Email!)
            }),
            
            Expires = DateTime.UtcNow.AddDays(7), 
            
            
           SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        // return Ok(new AuthResponseDto 
        // { 
        //     Token = tokenHandler.WriteToken(token), 
        //     Message = "Успешный вход!" 
        // });
        return Ok(tokenHandler.WriteToken(token));
    }
}