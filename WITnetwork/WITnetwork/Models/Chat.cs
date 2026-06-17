using WITnetwork.Models;

public class Chat
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    
    public Image? Avatar { get; set; }

    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public ICollection<UserProfile> Users { get; set; } = new List<UserProfile>();

    public required UserProfile Admin { get; set; }

    public Guid AdminId { get; set; }

    public bool IsGroup { get; set; }
}