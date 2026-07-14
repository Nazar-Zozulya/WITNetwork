
namespace WITnetwork.Models;

public class AlbumImage
{
    public long Id { get; set; }
    
    public string PublicId { get; set; } = string.Empty; 

    public string Image { get; set; } = string.Empty;
    

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public bool IsShown { get; set; }

    public Album? Album { get; set; }

    public long AlbumId { get; set; }
}