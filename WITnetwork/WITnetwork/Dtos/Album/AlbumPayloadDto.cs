using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record AlbumPayloadDto (
    [property: JsonPropertyName("id")] long AlbumId
);