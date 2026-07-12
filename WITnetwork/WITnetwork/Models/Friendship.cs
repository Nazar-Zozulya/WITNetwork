

using WITnetwork.Models;

public class Friendship
{
    public long Id { get; set; }

    public UserProfile From { get; set; } = null!;
    public long FromId { get; set; }

    public UserProfile To { get; set; } = null!;
    public long ToId { get; set; }

    public bool Status { get; set; } = false;


}