

using System.ComponentModel.DataAnnotations;

public record TagResponseDto (
    [ Required ] Guid Id,
    [ Required ] string Name
);