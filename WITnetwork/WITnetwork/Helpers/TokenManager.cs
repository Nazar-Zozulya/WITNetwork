


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WITnetwork.Models;

namespace WITnetwork.Helpers;


public class TokenManager
{
    public string GenerateToken(UserProfile user)
    {
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