using System.ComponentModel.DataAnnotations;

public record RegisterDto (
    [Required] string Email,
    [Required] string Password
);