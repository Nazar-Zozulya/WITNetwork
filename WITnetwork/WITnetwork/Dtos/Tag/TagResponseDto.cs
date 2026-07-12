

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public record TagResponseDto (
    [ property: JsonPropertyName("id") ] long Id,
    [ property: JsonPropertyName("name") ] string Name
);