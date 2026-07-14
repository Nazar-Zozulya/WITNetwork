using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record AlbumImagePayloadDto (
    [property: JsonPropertyName("albumId")] long AlbumId,
    [property: JsonPropertyName("imageId")] long ImageId

);