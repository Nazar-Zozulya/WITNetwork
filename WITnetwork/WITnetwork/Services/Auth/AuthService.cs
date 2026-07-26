



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
    public async Task<string> Create(CreateDto dto)
    {
        try{

            // створюю обьект нового користувача
            var NewUser = new UserProfile
            {
                Email = dto.Email, 
                UserName = $"user_{Guid.NewGuid()}",
                
                // PasswordHash = dto.Password,
            };

            // створюемо користувача в бд
            var result = await UserManager.CreateAsync(NewUser, dto.Password);

            // перевірка чи створився користувач
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ",
                    result.Errors.Select(e => e.Description)));
            }


            // створення профілю 
            var newProfile = new Models.Profile
            {
                IsImageSignature = false,
                IsTextSignature = false,
                UserId = NewUser.Id
            };

            context.Profiles.Add(newProfile);
            
            await context.SaveChangesAsync();

            var newMyAlbum = new Album
            {
                IsMyPhotoAlbum = true,
                Name = "",
                Theme = "",
                Year = DateTimeOffset.UtcNow.Year,
                ProfileId = newProfile.Id
                
            };

            context.Albums.Add(newMyAlbum);
            await context.SaveChangesAsync();





            // повертаю токен
            return tokenManager.GenerateToken(NewUser);
        }
        catch (Exception ex) 
        {
            throw new Exception(ex.ToString());
        }
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
        try
        {
            Console.WriteLine("1. Метод PreConfirmEmail вызван");

            Console.WriteLine("2. Поиск пользователя...");
            var findUser = await context.Users.FirstOrDefaultAsync(user => user.Email == dto.Email);
            Console.WriteLine("3. Поиск пользователя завершен");

            if (findUser != null)
            {
                Console.WriteLine("4. Пользователь уже существует");
                throw new Exception("User exists");
            }

            Console.WriteLine("5. Генерация кода");
            string code = new Random().Next(100000, 999999).ToString();

            Console.WriteLine($"6. Код сгенерирован: {code}");

            Console.WriteLine("7. Отправка письма...");
            var emailSent = await emailService.SendVerificationEmailAsync(new SendVerificationEmailDto
            {
                Email = dto.Email,
                VerificationCode = code
            });
            Console.WriteLine($"8. SendVerificationEmailAsync завершился. Результат: {emailSent}");

            if (!emailSent)
            {
                Console.WriteLine("9. Отправка письма не удалась");
                throw new Exception("Failed to send email");
            }

            Console.WriteLine("10. Создание объекта EmailVerification");

            var emailVerification = new EmailVerification
            {
                NewEmail = dto.Email,
                Code = code,
            };

            Console.WriteLine("11. Добавление в DbContext");

            await context.EmailVerifications.AddAsync(emailVerification);

            Console.WriteLine("12. Вызов SaveChangesAsync");

            await context.SaveChangesAsync();

            Console.WriteLine("13. SaveChangesAsync завершился");

            Console.WriteLine("14. Метод успешно завершен");

            return "лист відправлено";
        }
        catch (Exception ex)
        {
            Console.WriteLine("ОШИБКА:");
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<string> ConfirmEmail(ConfirmEmailDto dto)
    {
        // шукаємо код підтвердження в бд
        var findCode = await context.EmailVerifications.FirstOrDefaultAsync(code => code.NewEmail == dto.Email && code.Code == dto.Code.ToString());

        // перевіряємо чи є такий код
        if (findCode == null)
        {
            throw new Exception("Code not found");
        }

        return "Email confirmed";
    }
}