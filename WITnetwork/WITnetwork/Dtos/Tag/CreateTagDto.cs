

using System.ComponentModel.DataAnnotations;

public record CreateTagDto (
    [ Required ] string Name
);