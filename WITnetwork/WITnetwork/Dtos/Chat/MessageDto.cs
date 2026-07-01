




using WITnetwork.Models;

public record MessageDto (
    Guid Id,
    string? Text,
    Chat? Chat,
    Guid ChatId,
    DateTimeOffset CreatedAt,
    UserProfile? Sender,
    Guid? SenderId,
    ICollection<UserProfile> Readers
);