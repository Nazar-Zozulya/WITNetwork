public interface IChatService
{
    Task<Chat> GetChat(Guid userId, Guid anotherUserId);

    Task<IEnumerable<Chat>> GetIndividualChats(Guid userId );
}