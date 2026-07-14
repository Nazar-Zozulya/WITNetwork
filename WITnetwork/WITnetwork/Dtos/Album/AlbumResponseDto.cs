

using System.Text.Json.Serialization;
using WITnetwork.Models;

namespace WITnetwork.Dtos;

public record AlbumResponseDto (
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("theme")] string Theme,
    [property: JsonPropertyName("year")] int Year,
    [property: JsonPropertyName("created_at")] DateTimeOffset CreatedAt,
    [property: JsonPropertyName("is_shown")] bool IsShown,
    [property: JsonPropertyName("is_default")] bool IsDefault,
    [property: JsonPropertyName("images")] ICollection<AlbumImageDto> Images

);