using Microsoft.VisualBasic;

namespace WITnetwork.Models;

public class Post
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }

    public UserProfile Author { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public string? Topic { get; set; }

    public int AuthorId { get; set; }

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public ICollection<Image> Image { get; set; } = new List<Image>();
}
