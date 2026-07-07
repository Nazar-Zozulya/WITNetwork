using Microsoft.AspNetCore.Identity;

namespace WITnetwork.Models;

public class UserProfile : IdentityUser<long>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime? BirthDate { get; set; }

    public string? Pseudonym { get; set; }

    public string? Signature { get; set; }

    public bool IsImageSignature { get; set; }

    public bool IsTextSignature { get; set; }

    // public string? Avatar { get; set; }

    public string? Bio { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public string? Language { get; set; }

    public string? TimeZone { get; set; }

    public Profile? Profile { get; set; }
    public int ProfileId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();

}