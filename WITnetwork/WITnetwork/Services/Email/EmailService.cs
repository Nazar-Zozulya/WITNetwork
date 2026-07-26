using System.Text;
using Newtonsoft.Json;
using WITnetwork.Dtos;

namespace WITnetwork.Services;

public class EmailService : IEmailService
{
	private readonly HttpClient _httpClient;
	private readonly EmailSettings _settings;


	public EmailService(
		HttpClient httpClient,
		IConfiguration configuration
	)
	{
		_httpClient = httpClient;

		_settings = new EmailSettings
		{
			ApiEmailKey = configuration["EmailSettings:ApiKey"]!,
			EmailUser = configuration["EmailSettings:EmailUser"]!
		};
	}


	public async Task<bool> SendVerificationEmailAsync(
		SendVerificationEmailDto dto
	)
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

				htmlContent =
					$"<h2>Ваш код підтвердження: {dto.VerificationCode}</h2>"
			};


			var request = new HttpRequestMessage(
				HttpMethod.Post,
				"https://api.brevo.com/v3/smtp/email"
			);


			request.Headers.Add(
				"api-key",
				_settings.ApiEmailKey
			);


			request.Content = new StringContent(
				JsonConvert.SerializeObject(body),
				Encoding.UTF8,
				"application/json"
			);


			var response = await _httpClient.SendAsync(request);


			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine(
					await response.Content.ReadAsStringAsync()
				);

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