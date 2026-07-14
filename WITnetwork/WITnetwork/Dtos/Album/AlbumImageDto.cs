

using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record AlbumImageDto (
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("image")] string Image,
    [property: JsonPropertyName("created_at")] DateTimeOffset CreatedAt,
    [property: JsonPropertyName("is_shown")] bool IsShown,
    [property: JsonPropertyName("albumId")] int AlbumId

);