public record AddUsersRequestDto(
    Guid ChatId,
    List<Guid> UserIds
);