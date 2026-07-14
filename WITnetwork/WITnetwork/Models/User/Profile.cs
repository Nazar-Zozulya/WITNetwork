using WITnetwork.Models;

namespace WITnetwork.Models;

public class Profile
{
    public long Id { get; set; }

    public string? Signature { get; set; }

    public DateTimeOffset? BirthDate { get; set; }

    public UserProfile? User { get; set; }
    
    public long UserId { get; set; }

    public string? Avatar { get; set; }

    public string? Pseudonym { get; set; }

    public bool IsImageSignature { get; set; }

    public bool IsTextSignature { get; set; }

    public ICollection<Album> Albums {get; set;} = new List<Album>();
}