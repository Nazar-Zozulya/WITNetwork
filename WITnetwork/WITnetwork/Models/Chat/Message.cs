

using WITnetwork.Models;

public class Message
{
    public long Id { get; set; }

    public string? Text { get; set; }

    public Chat? Chat { get; set; }
    public long ChatId { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public UserProfile? Sender { get; set; }

    public long? SenderId { get; set; }

    // public ICollection<UserProfile> Readers { get; set; } = new List<UserProfile>();
    public List<UserProfile> Readers { get; set; } = new List<UserProfile>();
}