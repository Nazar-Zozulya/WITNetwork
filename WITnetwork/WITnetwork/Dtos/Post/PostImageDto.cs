using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WITnetwork.Dtos;

public record PostImageDto (
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("original_image")] string OriginalImage,
    [property: JsonPropertyName("compressed_image")] string CompressedImage
);