using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WITnetwork.Dtos;
using Microsoft.Extensions.Options;

namespace WITnetwork.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    private readonly HttpClient _httpClient;

    public EmailService(
        IOptions<EmailSettings> options,
        HttpClient httpClient
    )
    {
        _settings = options.Value;
        _httpClient = httpClient;
    }

    public async Task<bool> SendVerificationEmailAsync(SendVerificationEmailDto dto)
    {
        try
        {
            var request = new
            {
                from = _settings.EmailUser,
                to = new[] { dto.Email },
                subject = "Верифікація пошти",
                html = $"Ваш код підтвердження: {dto.VerificationCode}"
            };

            var json = JsonSerializer.Serialize(request);

            var message = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.resend.com/emails"
            );

            message.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    _settings.ApiEmailKey
                );

            message.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.SendAsync(message);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(
                    $"Resend error: {response.StatusCode}"
                );

                Console.WriteLine(
                    await response.Content.ReadAsStringAsync()
                );

                return false;
            }

            Console.WriteLine("EMAIL: письмо отправлено через Resend");

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