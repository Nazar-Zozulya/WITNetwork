public record AddUsersRequestDto(
    long ChatId,
    List<long> UserIds
);