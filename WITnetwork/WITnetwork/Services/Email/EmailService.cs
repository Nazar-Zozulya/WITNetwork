using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using WITnetwork.Dtos;

namespace WITnetwork.Services;

public class EmailService : IEmailService
{
	private readonly HttpClient _httpClient;
	private readonly EmailSettings _settings;

	public EmailService(
		HttpClient httpClient,
		IOptions<EmailSettings> options
	)
	{
		_httpClient = httpClient;
		_settings = options.Value;
	}


	public async Task<bool> SendVerificationEmailAsync(SendVerificationEmailDto dto)
	{
		try
		{
			var body = new
			{
				sender = new
				{
					name = "WITnetwork",
					email = _settings.EmailUser
				},
				to = new[]
				{
					new
					{
						email = dto.Email
					}
				},
				subject = "Верифікація пошти",
				htmlContent = $"<h2>Ваш код підтвердження: {dto.VerificationCode}</h2>"
			};


			var json = JsonSerializer.Serialize(body);

			var request = new HttpRequestMessage(
				HttpMethod.Post,
				"https://api.brevo.com/v3/smtp/email"
			);


			request.Headers.Add(
				"api-key",
				_settings.ApiEmailKey
			);


			request.Content = new StringContent(
				json,
				Encoding.UTF8,
				"application/json"
			);


			var response = await _httpClient.SendAsync(request);


			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync();

				Console.WriteLine(error);

				return false;
			}


			return true;
		}
		catch(Exception ex)
		{
			Console.WriteLine(ex);
			return false;
		}
	}
}