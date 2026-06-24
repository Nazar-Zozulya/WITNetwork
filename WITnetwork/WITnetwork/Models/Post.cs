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

    public ICollection<PostImage> Images { get; set; } = new List<PostImage>();

}


public class PostImage
{
    public Guid Id { get; set; }

    public Guid PostId { get; set; }

    public Post? Post { get; set; }

    public string Url { get; set; } = string.Empty;

    public string PublicId { get; set; } = string.Empty;
}