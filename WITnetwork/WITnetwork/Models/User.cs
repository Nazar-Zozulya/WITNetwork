using Microsoft.AspNetCore.Identity;

namespace WITnetwork.Models;

public class User: IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public DateTime? BirthOfDate { get; set; }

    public string? signature { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();

}
