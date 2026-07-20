


using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record SeeMessageDto (
    [property: JsonPropertyName("messageId")] long MessageId,
    [property: JsonPropertyName("readerId")] long ReaderId
);