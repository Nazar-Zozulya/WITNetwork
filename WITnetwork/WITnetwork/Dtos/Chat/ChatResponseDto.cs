using WITnetwork.Models;

public record ChatResponseDto(
    Guid Id,
    string? Name,
    bool IsGroup,
    Image? Avatar,
    List<Message> Messages,
    List<UserProfile> Users,
    UserProfile Admin,
    Guid AdminId
);