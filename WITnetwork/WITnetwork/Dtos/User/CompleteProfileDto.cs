
using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record CompleteProfileDto (
    [property: JsonPropertyName("first_name")] string? FirstName,
    [property: JsonPropertyName("last_name")] string? LastName,
    [property: JsonPropertyName("username")] string? Username
);