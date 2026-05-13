    namespace WITnetwork.Models;

public class Image
{
    public Guid Id { get; set; }
    public string File { get; set; } = string.Empty;

    public string Filename { get; set; } = string.Empty;

    public string Base64 { get; set; } = string.Empty;


    public DateTimeOffset UploadedAt { get; set; } = DateTimeOffset.UtcNow;
}
