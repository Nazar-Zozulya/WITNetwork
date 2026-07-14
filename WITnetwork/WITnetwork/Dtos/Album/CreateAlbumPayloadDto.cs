

using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record CreateAlbumPayloadDto (
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("theme")] string Theme,
    [property: JsonPropertyName("year")] int Year
);