
namespace WITnetwork.Models;

public class Album
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Theme { get; set; } = string.Empty;

    public int Year { get; set; }

    public DateTimeOffset CreatedAT { get; set; } = DateTimeOffset.UtcNow;

    public bool IsShown { get; set; }

    public bool IsDefault { get; set; }

    public Profile? Profile { get; set; }

    public long ProfileId { get; set; }


    public ICollection<AlbumImage> Images { get; set; } = new List<AlbumImage>();
}