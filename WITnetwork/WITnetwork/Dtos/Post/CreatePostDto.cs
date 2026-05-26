
using System.ComponentModel.DataAnnotations;

public record CreatePostDto (
    [ Required ] string Title,
    string? Content,
    string? Topic,
    ICollection<Guid> TagIds
);