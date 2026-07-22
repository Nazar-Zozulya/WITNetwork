using System.Text.Json.Serialization;

public record DeletePostDto (
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("userId")] long UserId
);