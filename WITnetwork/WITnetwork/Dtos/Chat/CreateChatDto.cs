using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record CreateChatDto (
    [property: JsonPropertyName("userId")] long UserId,
    [property: JsonPropertyName("anotherUserId")] long AnotherUserId
);