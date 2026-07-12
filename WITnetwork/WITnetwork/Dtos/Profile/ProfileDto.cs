using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;
public record ProfileDto (
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("signature")] string? Signature,
    [property: JsonPropertyName("birth_date")] DateTimeOffset BirthDate,
    // [property: JsonPropertyName("user")] UserResponseDto User,
    [property: JsonPropertyName("user_id")] long UserId,
    [property: JsonPropertyName("avatar")] string? Avatar,
    [property: JsonPropertyName("pseudonym")] string? Pseudonym
);


