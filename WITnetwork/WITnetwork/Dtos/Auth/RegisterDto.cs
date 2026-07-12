using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;


public record CreateDto(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("password")] string Password
);