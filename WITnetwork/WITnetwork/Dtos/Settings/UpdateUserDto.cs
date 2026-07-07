using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record UpdateUserDto(
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("avatar")] string? Avatar,
    [property: JsonPropertyName("email")] string? Email,
    [property: JsonPropertyName("username")] string? Username,
    [property: JsonPropertyName("password")] string? Password,
    [property: JsonPropertyName("birth_date")] DateTime? BirthDate,
    [property: JsonPropertyName("first_name")] string? FirstName,
    [property: JsonPropertyName("last_name")] string? LastName
);