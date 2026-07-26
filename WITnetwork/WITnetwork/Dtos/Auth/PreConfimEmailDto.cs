using System.Text.Json.Serialization;

namespace WITnetwork.Dtos;


public record PreConfirmEmailDto (
    [property: JsonPropertyName("email")]string Email
);