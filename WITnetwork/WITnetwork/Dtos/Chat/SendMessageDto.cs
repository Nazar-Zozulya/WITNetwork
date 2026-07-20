





using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record SendMessageDto (
    [property: JsonPropertyName("chatId")] long ChatId,
    [property: JsonPropertyName("receiverId")] long ReceiverId,
    [property: JsonPropertyName("senderId")] long SenderId,
    [property: JsonPropertyName("Text")] string Text
);