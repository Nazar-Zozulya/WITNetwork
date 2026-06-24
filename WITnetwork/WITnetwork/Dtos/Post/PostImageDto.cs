using System.ComponentModel.DataAnnotations;

namespace WITnetwork.Dtos;

public record PostImageDto (
    [Required] string Url
);