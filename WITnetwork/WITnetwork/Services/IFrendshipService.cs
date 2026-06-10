

using WITnetwork.Models;

public interface IFriendshipService
{
    Task<AppResult> GetFriendshipsAsync(Guid userId);
    Task<AppResult> GetFriendRequestsAsync(Guid userId);
    Task<AppResult> GetFriendRecommendationsAsync(Guid userId);
    Task<AppResult> SendFriendRequestAsync(Guid userId, Guid receiverId);
    Task<AppResult> AcceptFriendRequestAsync(Guid userId, Guid receiverId);
    Task<AppResult> DeleteFriendRelationshipAsync(Guid userId, Guid receiverId);

}