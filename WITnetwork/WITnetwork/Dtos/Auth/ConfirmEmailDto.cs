using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record ConfirmEmailDto (
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("code")] int Code
);