

using WITnetwork.Dtos;
using WITnetwork.Models;

public interface IFriendshipService
{
    Task<IEnumerable<UserWithoutIncludes>> GetFriendshipsAsync(long userId, int page, int size);
    Task<IEnumerable<UserWithoutIncludes>> GetFriendRequestsAsync(long userId, int page, int size);
    Task<IEnumerable<UserWithoutIncludes>> GetFriendRecommendationsAsync(long userId, int page, int size);
    Task<string> SendFriendRequestAsync(long userId, long receiverId);
    Task<string> AcceptFriendRequestAsync(long userId, long receiverId);
    Task<string> DeleteFriendRelationshipAsync(long userId, long receiverId);
    Task<string> WhichFriendshipAsync(long myUserId, long anotherUserId);

}