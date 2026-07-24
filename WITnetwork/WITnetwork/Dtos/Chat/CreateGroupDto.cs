

using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record CreateGroupDto (
    [property: JsonPropertyName("users")] IEnumerable<UserWithoutIncludes> Users,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("adminId")] long AdminId,
    [property: JsonPropertyName("avatar")] string? Avatar
);