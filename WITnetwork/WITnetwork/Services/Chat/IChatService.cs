using WITnetwork.Models;

public interface IChatService
{
    Task<Chat> GetChat(long userId, long anotherUserId);

    Task<IEnumerable<Chat>> GetIndividualChats(long userId );
    Task<Chat> AddUsersToChatAsync(long chatId, long adminId, List<long> userIds);
}