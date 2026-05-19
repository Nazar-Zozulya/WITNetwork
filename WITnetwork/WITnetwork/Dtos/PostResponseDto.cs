



using System.ComponentModel.DataAnnotations;

public record PostResponseDto
(
    [ Required ] Guid Id,
    [ Required ] string Title,
    string? Content,
    string? Topic,
    DateTimeOffset CreatedAt
);