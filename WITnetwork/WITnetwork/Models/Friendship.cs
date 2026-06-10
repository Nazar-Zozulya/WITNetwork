

using WITnetwork.Models;

public class Friendship
{
    public Guid Id { get; set; }

    public UserProfile From { get; set; } = null!;
    public Guid FromId { get; set; }

    public UserProfile To { get; set; } = null!;
    public Guid ToId { get; set; }

    public bool Status { get; set; } = false;


}