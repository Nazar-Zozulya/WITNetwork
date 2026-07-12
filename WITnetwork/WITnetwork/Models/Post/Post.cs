using Microsoft.VisualBasic;

namespace WITnetwork.Models;

public class Post
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }

    public UserProfile? Author { get; set; }
    
    public long AuthorId { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public string? Topic { get; set; }


    public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public ICollection<PostImage> Images { get; set; } = new List<PostImage>();

}


public class PostImage
{
    public long Id { get; set; }

    public long PostId { get; set; }

    public Post? Post { get; set; }

    public string OriginalImage { get; set; } = string.Empty;

    public string? CompressedImage { get; set; } = string.Empty;

    public string PublicId { get; set; } = string.Empty;
}