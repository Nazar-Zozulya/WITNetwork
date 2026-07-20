using System.Text.Json.Serialization;
using WITnetwork.Models;

namespace WITnetwork.Dtos;

public record ChatResponseDto(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("is_group")] bool IsGroup,
    // [property: JsonPropertyName("avatar")] string? Avatar,
    // [property: JsonPropertyName("avatarId")] string? AvatarId,
    [property: JsonPropertyName("messages")] List<MessageDto> Messages,
    [property: JsonPropertyName("users")] List<UserToChatDto> Users,
    [property: JsonPropertyName("admin")] UserToChatDto Admin,
    [property: JsonPropertyName("adminId")] long AdminId
);