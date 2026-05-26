using System.ComponentModel.DataAnnotations;

public record LoginDto (
    [Required] string Email,
    [Required] string Password
);