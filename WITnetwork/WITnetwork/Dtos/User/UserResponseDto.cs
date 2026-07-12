


using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record UserResponseDto (
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("username")] string UserName,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("password")] string PasswordHash,
    [property: JsonPropertyName("first_name")] string? FirstName,
    [property: JsonPropertyName("last_name")] string? LastName,
    // [property: JsonPropertyName("is_active")] bool IsActive,
    // [property: JsonPropertyName("is_staff")] bool IsStaff,
    // [property: JsonPropertyName("is_superuser")] bool IsSuperUser,
    [property: JsonPropertyName("last_login")] DateTimeOffset? LastLoginAt,
    [property: JsonPropertyName("date_joined")] DateTimeOffset DateJoined,
    [property: JsonPropertyName("profile")] ProfileDto Profile
    
);