




using WITnetwork.Models;

public record MessageDto (
    long Id,
    string? Text,
    Chat? Chat,
    long ChatId,
    DateTimeOffset CreatedAt,
    UserProfile? Sender,
    long? SenderId,
    ICollection<UserProfile> Readers
);