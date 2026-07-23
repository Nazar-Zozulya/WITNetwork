using WITnetwork.Dtos;
using WITnetwork.Models;

namespace WITnetwork.Services;

public interface IChatService
{
    Task<ChatResponseDto> GetChat(long userId, long anotherUserId);

    Task<IEnumerable<ChatResponseDto>> GetIndividualChats(long userId, int page, int size );
    Task<ChatResponseDto> AddUsersToChatAsync(long chatId, long adminId, List<long> userIds);

    Task<IEnumerable<MessageDto>> GetMessagesFromChat(long chatId, int page, int size);
}