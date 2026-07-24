


using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record UserWithoutIncludes (
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("username")] string UserName,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("first_name")] string? FirstName,
    [property: JsonPropertyName("last_name")] string? LastName,
    [property: JsonPropertyName("last_login")] DateTimeOffset? LastLoginAt,
    [property: JsonPropertyName("date_joined")] DateTimeOffset DateJoined
);