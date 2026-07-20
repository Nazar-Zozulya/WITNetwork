

using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record UserToChatDto
(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("first_name")] string? FirstName,
    [property: JsonPropertyName("last_name")] string? LastName,
    [property: JsonPropertyName("username")] string username
);