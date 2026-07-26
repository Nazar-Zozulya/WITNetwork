

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
            Console.WriteLine("EMAIL: создание сообщения");

            var message = new MimeMessage();

            message.From.Add(
                new MailboxAddress("Имя Отправителя", _settings.EmailUser)
            );

            message.To.Add(
                new MailboxAddress("Имя Получателя", dto.Email)
            );

            message.Subject = "Верифікація пошти";

            message.Body = new TextPart("plain")
            {
                Text = $"Ваш код подтверждения: {dto.VerificationCode}"
            };


            Console.WriteLine("EMAIL: подключение SMTP");

            using var client = new SmtpClient();

            await client.ConnectAsync(
                "smtp.gmail.com",
                587,
                MailKit.Security.SecureSocketOptions.StartTls
            );

            Console.WriteLine("EMAIL: SMTP подключен");


            await client.AuthenticateAsync(
                _settings.EmailUser,
                _settings.EmailPass
            );

            Console.WriteLine("EMAIL: авторизация успешна");


            await client.SendAsync(message);

            Console.WriteLine("EMAIL: письмо отправлено");


            await client.DisconnectAsync(true);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("EMAIL ERROR:");
            Console.WriteLine(ex);

            return false;
        }
    }
}