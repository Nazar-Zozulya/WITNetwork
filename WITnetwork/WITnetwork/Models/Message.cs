

using WITnetwork.Models;

public class Message
{
    public Guid Id { get; set; }

    public string? Text { get; set; }

    public Chat? Chat { get; set; }
    public Guid ChatId { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public required UserProfile Sender { get; set; }

    public Guid SenderId { get; set; }

    public ICollection<UserProfile> Readers { get; set; } = new List<UserProfile>();
}