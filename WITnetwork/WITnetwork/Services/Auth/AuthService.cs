



using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WITnetwork.Data;
using WITnetwork.Dtos;
using WITnetwork.Helpers;
using WITnetwork.Models;


namespace WITnetwork.Services;

public class AuthService(NetworkDBContext context, IMapper mapper, UserManager<UserProfile> UserManager, TokenManager tokenManager, IEmailService emailService) : IAuthService
{
    public async Task<string> Register(RegisterDto dto)
    {
        // створюю обьект нового користувача
        var NewUser = new UserProfile
        {
            Email = dto.Email, 
            UserName = $"user_{Guid.NewGuid()}",
            PasswordHash = dto.Password,
        };

        // створюемо користувача в бд
        var result = await UserManager.CreateAsync(NewUser, dto.Password);

        // перевірка чи створився користувач
        if (!result.Succeeded)
        {
            throw new Exception("Failed to create user");
        }

        // повертаю токен
        return tokenManager.GenerateToken(NewUser);
    }


    public async Task<string> Login(LoginDto dto)
    {
        // шукаємо користувача по email
        var findUser = await context.Users.FirstOrDefaultAsync(user => user.Email == dto.Email);

        // перевіряємо чи є такий юзер
        if (findUser == null)
        {
            throw new Exception("User not found");
        }

        // перевіряємо чи пароль правильний
        if (!await UserManager.CheckPasswordAsync(findUser, dto.Password))
        {
            throw new Exception("password is incorrect");
        }

        // повертаю токен
        return tokenManager.GenerateToken(findUser);
    }

    public async Task<string> PreConfirmEmail(PreConfirmEmailDto dto)
    {
        // шукаємо користувача по email
        var findUser = await context.Users.FirstOrDefaultAsync(user => user.Email == dto.Email);

        // перевіряємо чи є такий юзер
        if (findUser != null)
        {
            throw new Exception("User not found");
        } 

        // створюємо код підтвердження
        string code = new Random().Next(100000, 999999).ToString();


        // відправляємо емейл

        var emailSent = await emailService.SendVerificationEmailAsync(new SendVerififcationEmailDto
        {
            Email = dto.Email,
            VerificationCode = code
        });

        if (!emailSent)
        {
            throw new Exception("Failed to send email");
        }





        // якщо емейл відправився то зберігаемо код в базу даних

        // створюємо обьект коду підтвердження
        var emailVerification = new EmailVerification
        {
            NewEmail = dto.Email,
            Code = code,
        };

        // добавляемо його в базу даних
        await context.EmailVerifications.AddAsync(emailVerification);

        // зберігаемо зміни в базу даних
        await context.SaveChangesAsync();


        return "лист відправлено";
    }

    public async Task<string> ConfirmEmail(ConfirmEmailDto dto)
    {
        // шукаємо код підтвердження в бд
        var findCode = await context.EmailVerifications.FirstOrDefaultAsync(code => code.NewEmail == dto.Email && code.Code == dto.Code);

        // перевіряємо чи є такий код
        if (findCode == null)
        {
            throw new Exception("Code not found");
        }

        return "Email confirmed";
    }
}