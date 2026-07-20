using System.Text.Json.Serialization;
using WITnetwork.Models;

namespace WITnetwork.Dtos;

public record MessageDto (
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("text")] string Text,
    [property: JsonPropertyName("created_at")] DateTimeOffset CreatedAt,
    [property: JsonPropertyName("sender")] UserToChatDto? Sender,
    [property: JsonPropertyName("senderId")] long? SenderId,
    [property: JsonPropertyName("readers")] ICollection<UserToChatDto> Readers,
    // [property: JsonPropertyName("Images")] ... Image
    // [property: JsonPropertyName("chat")]ChatResponseDto? Chat,
    [property: JsonPropertyName("chatId")]long ChatId
);