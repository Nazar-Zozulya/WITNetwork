

using WITnetwork.Dtos;
using WITnetwork.Models;

public interface IFriendshipService
{
    Task<IEnumerable<UserResponseDto>> GetFriendshipsAsync(long userId, int page, int size);
    Task<IEnumerable<UserResponseDto>> GetFriendRequestsAsync(long userId, int page, int size);
    Task<IEnumerable<UserResponseDto>> GetFriendRecommendationsAsync(long userId, int page, int size);
    Task<string> SendFriendRequestAsync(long userId, long receiverId);
    Task<string> AcceptFriendRequestAsync(long userId, long receiverId);
    Task<string> DeleteFriendRelationshipAsync(long userId, long receiverId);
    Task<string> WhichFriendshipAsync(long myUserId, long anotherUserId);

}