



using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Org.BouncyCastle.Crypto.Digests;

namespace WITnetwork.Dtos;

public record PostResponseDto
(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("content")] string? Content,
    [property: JsonPropertyName("topic")] string? Topic,
    [property: JsonPropertyName("created_at")] DateTimeOffset CreatedAt,
    [property: JsonPropertyName("updated_at")] DateTimeOffset UpdatedAt,
    [property: JsonPropertyName("author")] UserToPostDto Author,
    [property: JsonPropertyName("authorId")] int AuthorId,
    [property: JsonPropertyName("images")] IEnumerable<PostImageDto> Images,
    [property: JsonPropertyName("tags")] IEnumerable<TagResponseDto> Tags
);