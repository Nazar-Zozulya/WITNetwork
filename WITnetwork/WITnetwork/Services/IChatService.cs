public interface IChatService
{
    Task<Chat> GetChat(Guid userId, Guid anotherUserId);

    Task<IEnumerable<Chat>> GetIndividualChats(Guid userId );
    Task<Chat> AddUsersToChatAsync(Guid chatId, Guid adminId, List<Guid> userIds);
}