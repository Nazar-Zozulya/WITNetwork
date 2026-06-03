



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
            // PasswordHash = dto.Password,
        };

        var result = await UserManager.CreateAsync(NewUser, dto.Password);
        return result;
    }

    public async Task<string?> Login(LoginDto dto, UserManager<UserProfile> UserManager)
    {
        var findUser = await context.Users.FirstOrDefaultAsync(user => user.Email == dto.Email);
        if (findUser == null)
        {
            return null;
        }

        if (await UserManager.CheckPasswordAsync(findUser, dto.Password))
        {
            return null;
        }

        var token = new JwtSecurityToken(
            issuer: "AuthServer",     
            audience: "BackendApi",
            claims: new[] { new Claim(ClaimTypes.NameIdentifier, findUser.Id.ToString()) },
            expires: DateTime.UtcNow.AddYears(2),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("12345789012345789012345789012")), SecurityAlgorithms.HmacSha256) // Подпись
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}