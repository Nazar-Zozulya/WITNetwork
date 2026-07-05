namespace WITnetwork.Models;

public class EmailVerification
{
    public long Id { get; set; }
    public string NewEmail { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; set; } = DateTimeOffset.UtcNow.AddMinutes(5);
}