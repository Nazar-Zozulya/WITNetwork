using Microsoft.AspNetCore.Identity;

namespace WITnetwork.Models;

public class UserProfile: IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public DateTime? BirthDate { get; set; }

    public string? Pseudonym { get; set; }

    public string? Signature { get; set; }

    public bool? IsImageSignature { get; set; }

    public bool? IsTextSignature { get; set; }

    public string? Avatar { get; set; }

    // public ICollection<Chat> Chats { get; set; }

    // public ICollection<Chat> ChatsWhereAdmin { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();

}
