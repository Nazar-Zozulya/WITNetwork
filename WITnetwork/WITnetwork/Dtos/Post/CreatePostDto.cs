
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;

public record CreatePostDto (
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("content")] string Content,
    [property: JsonPropertyName("author")] UserToPostDto Author,
    [property: JsonPropertyName("images")] List<string>? Images,
    [property: JsonPropertyName("tags")] List<string>? Tags,
    [property: JsonPropertyName("links")] List<string>? Links
);