

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WITnetwork.Data;
using WITnetwork.Models;

public class FriendshipService(NetworkDBContext context, IMapper mapper) : IFriendshipService
{
    public async Task<AppResult> GetFriendshipsAsync(Guid userId)
    {   
        var friendships = await context.Friendships
            .Include(f => f.From)
            .Include(f => f.To)
            .Where(f => f.Status == true && 
                (f.FromId == userId || f.ToId == userId))
            .ToListAsync();


        var friends = friendships.Select(f =>
            f.FromId == userId ? f.To : f.From);

        if (friends.IsNullOrEmpty())
        {
            return Result.Error("friends not found");   
        }
        return Result.Success(friends);

    }

    public async Task<AppResult> GetFriendRequestsAsync(Guid userId)
    {   
        var friendships = await context.Friendships
            .Include(f => f.From)
            .Include(f => f.To)
            .Where(f => f.Status == false && 
                (f.ToId == userId))
            .ToListAsync();


        var friends = friendships.Select(f =>
            f.FromId == userId ? f.To : f.From);

        if (friends.IsNullOrEmpty())
        {
            return Result.Error("requests not found");
            
        }
        return Result.Success(friends);

    }

    public async Task<AppResult> GetFriendRecommendationsAsync(Guid userId)
    {   
        var friendships = await context.Friendships
            .Include(f => f.From)
            .Include(f => f.To)
            .Where(f => f.Status == false && 
                (f.FromId != userId || f.ToId != userId))
            .ToListAsync();


        var friends = friendships.Select(f =>
            f.FromId == userId ? f.To : f.From);

        if (friends.IsNullOrEmpty())
        {
            return Result.Error("recomendations not found");
        }
        return Result.Success(friends);

    }

    public async Task<AppResult> SendFriendRequestAsync(Guid userId, Guid receiverId)
    {


        var newFriendship = context.Friendships
            .Add(new  Friendship
            {
                Status = false,
                FromId = userId,
                ToId = receiverId
            });
        await context.SaveChangesAsync();

        var findFriendship = await context.Friendships
            .FirstOrDefaultAsync(f =>
                f.Status == false &&
                f.FromId == userId &&
                f.ToId == receiverId
            );

        if (findFriendship != null)
        {
            return Result.Success("request created succesfully");
        }
        return Result.Error("123");
    }

    public async Task<AppResult> AcceptFriendRequestAsync(Guid userId, Guid receiverId)
    {


        var updatedFriendship = await context.Friendships
            .Where(f => f.Status == false && f.FromId == receiverId && f.ToId == userId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(f => f.Status, true));
        await context.SaveChangesAsync();

        var findFriendship = await context.Friendships
            .FirstOrDefaultAsync(f =>
                f.Status == true &&
                f.FromId == receiverId &&
                f.ToId == userId
            );

        if (findFriendship != null)
        {
            return Result.Success("request created succesfully");
        }
        return Result.Error("123");
    }

    public async Task<AppResult> DeleteFriendRelationshipAsync(Guid userId, Guid receiverId)
    {


        var deletedFriendship = await context.Friendships
            .Where(f => f.Status == false && f.FromId == receiverId && f.ToId == userId)
            .ExecuteDeleteAsync();
        await context.SaveChangesAsync();

        var findFriendship = await context.Friendships
            .FirstOrDefaultAsync(f =>
                f.Status == true &&
                f.FromId == userId &&
                f.ToId == receiverId
            );

        if (findFriendship != null)
        {
            return Result.Success("request created succesfully");
        }
        return Result.Error("123");
    }


}