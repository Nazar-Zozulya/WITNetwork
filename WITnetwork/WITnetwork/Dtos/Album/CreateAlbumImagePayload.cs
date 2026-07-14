
using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record CreateAlbumImagePayloadDto (
    [property: JsonPropertyName("albumId")] long AlbumId,
    [property: JsonPropertyName("image")] string Image

);