

using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record UpdateAlbumPayloadDto (
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("theme")] string? Theme,
    [property: JsonPropertyName("year")] int? Year
);