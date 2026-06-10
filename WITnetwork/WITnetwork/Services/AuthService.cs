



using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WITnetwork.Data;
using WITnetwork.Models;

public class AuthService(NetworkDBContext context, IMapper mapper) : IAuthService
{
    public async Task<IdentityResult?> Register(RegisterDto dto, UserManager<UserProfile> UserManager)
    {
        var NewUser = new UserProfile{
            Email = dto.Email,
            UserName = $"user_{Guid.NewGuid()}",
            PasswordHash = dto.Password,
        };

        var result = await UserManager.CreateAsync(NewUser, dto.Password);
        return result;
    }

    public async Task<string?> Login(LoginDto dto, UserManager<UserProfile> UserManager, UserProfile user)
    {
        var findUser = await context.Users.FirstOrDefaultAsync(user => user.Email == dto.Email);
        if (findUser == null)
        {
            throw new UnauthorizedAccessException();
        }

        if (!await UserManager.CheckPasswordAsync(findUser, dto.Password))
        {
            throw new UnauthorizedAccessException();
        }

         var tokenHandler = new JwtSecurityTokenHandler();
        
        var key = Encoding.UTF8.GetBytes("SuperSecretKeyForDevelopment12345!SuperSecretKey");
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!)
            }),
            
            Expires = DateTime.UtcNow.AddDays(30), 
            
            
           SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
    

        return tokenHandler.WriteToken(token);
    }
}