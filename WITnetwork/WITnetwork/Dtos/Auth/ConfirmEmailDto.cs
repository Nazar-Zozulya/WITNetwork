namespace WITnetwork.Dtos;

public record ConfirmEmailDto (
    string Email,
    string Code
);