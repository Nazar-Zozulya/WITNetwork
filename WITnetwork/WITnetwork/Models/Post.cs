using Microsoft.VisualBasic;

namespace WITnetwork.Models;

public class Post
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }

    public UserProfile? Author { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public string? Topic { get; set; }

    public Guid AuthorId { get; set; }

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public ICollection<Image> Image { get; set; } = new List<Image>();
}
