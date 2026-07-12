

using WITnetwork.Dtos;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;

namespace WITnetwork.Services;

public class EmailService : IEmailService
{


    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task<bool> SendVerificationEmailAsync(SendVerificationEmailDto dto)
    {
        
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Имя Отправителя", _settings.EmailUser));
            message.To.Add(new MailboxAddress("Имя Получателя", dto.Email));
            message.Subject = "Верифікація пошти";
            message.Body = new TextPart("plain") { Text = $"Ваш код подтверждения: {dto.VerificationCode}" };

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.Auto);
            client.Authenticate(_settings.EmailUser, _settings.EmailPass);
            client.Send(message);
            client.Disconnect(true);

            return true;
        } catch (Exception ex)
        {
            // Обработка ошибки отправки письма
            Console.WriteLine($"Ошибка отправки письма: {ex.Message}");
            return false;
        }
    }
}