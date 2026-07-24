using WITnetwork.Models;

namespace WITnetwork.Models;

public class Chat
{
    public long Id { get; set; }
    public string? Name { get; set; }
    
    public string? AvatarUrl { get; set; }

    public string? AvatarPublicId { get; set; }

    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public ICollection<UserProfile> Users { get; set; } = new List<UserProfile>();

    public UserProfile? Admin { get; set; }

    public long AdminId { get; set; }

    public bool IsGroup { get; set; }
}