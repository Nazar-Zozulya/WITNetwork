

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WITnetwork.Data;
using WITnetwork.Dtos;
using WITnetwork.Models;

public class FriendshipService(NetworkDBContext context, IMapper mapper) : IFriendshipService
{
    public async Task<IEnumerable<UserWithoutIncludes>> GetFriendshipsAsync(long userId)
    {   
        try
        {      
            var friendships = await context.Friendships
                .Include(f => f.From)
                .Include(f => f.To)
                .Where(f => f.Status == true && 
                    (f.FromId == userId || f.ToId == userId))
                .ToListAsync();


            var friends = friendships.Select(f =>
                f.FromId == userId ? f.To : f.From);
            if (!friends.Any())
            {
                return Enumerable.Empty<UserWithoutIncludes>();
            }
            // if (friends.IsNullOrEmpty())
            // {
            //     throw new Exception("friends not found");   
            // }

            var mappedFriends = mapper.Map<IEnumerable<UserWithoutIncludes>>(friends);

            return mappedFriends;
        }
        catch  (Exception ex)
        {
            throw new Exception($"{ex}");
        }
    }

    public async Task<IEnumerable<UserWithoutIncludes>> GetFriendRequestsAsync(long userId)
    {   
        try {
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
                throw new Exception("requests not found");
                
            }
            
            var mappedFriends = mapper.Map<IEnumerable<UserWithoutIncludes>>(friends);

            return mappedFriends;
        }
        catch  (Exception ex)
        {
            throw new Exception($"{ex}");
        }

    }

    public async Task<IEnumerable<UserWithoutIncludes>> GetFriendRecommendationsAsync(long userId)
    {   
        try {
            // var user = await context.Users
            //     .Include(u => u.FriendshipsTo)
            //     .Include(u => u.FriendshipsFrom)
            //     .FirstOrDefaultAsync(u => u.Id == userId);


            var users = await context.Users
                .Include(u => u.FriendshipsTo)
                .Include(u => u.FriendshipsFrom)
                .Where(u => u.Id != userId &&
                    !u.FriendshipsFrom.Any(f => f.ToId == userId) &&
                    !u.FriendshipsTo.Any(f => f.FromId == userId))
                .ToListAsync();

            // var friendships = await context.Friendships
            //     .Include(f => f.From)
            //     .Include(f => f.To)
            //     .Where(f => f.FromId != userId || f.ToId != userId)
            //     .ToListAsync();


            // var friends = friendships.Select(f =>
            //     f.FromId == userId ? f.To : f.From);

            if (users.IsNullOrEmpty())
            {
                throw new Exception("recomendations not found");
            }

            var mappedUsers = mapper.Map<IEnumerable<UserWithoutIncludes>>(users);

            return mappedUsers;
        }
        catch  (Exception ex)
        {
            throw new Exception($"{ex}");
        }

    }

    public async Task<string> SendFriendRequestAsync(long userId, long receiverId)
    {
        try {
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
                return "request created succesfully";
            }
            throw new Exception("send friendship error");
        }
        catch  (Exception ex)
        {
            throw new Exception($"{ex}");
        }
    }

    public async Task<string> AcceptFriendRequestAsync(long userId, long receiverId)
    {
        try {
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
                return "request created succesfully";
            }
            throw new Exception("accepting friendship error");
        }
        catch  (Exception ex)
        {
            throw new Exception($"{ex}");
        }
    }

    public async Task<string> DeleteFriendRelationshipAsync(long userId, long receiverId)
    {
        try {
            var deletedFriendship = await context.Friendships
                .Where(f => f.FromId == receiverId && f.ToId == userId || f.FromId == userId && f.ToId == receiverId)
                .ExecuteDeleteAsync();
            await context.SaveChangesAsync();

            var findFriendship = await context.Friendships
                .FirstOrDefaultAsync(f =>
                    f.Status == true &&
                    f.FromId == userId &&
                    f.ToId == receiverId
                );

            if (findFriendship == null)
            {
                return "friendship deleted succesfully";
            }
            throw new Exception("deleting friendship error");
        }
        catch  (Exception ex)
        {
            throw new Exception($"{ex}");
        }
    }

    public async Task<string> WhichFriendshipAsync(long myUserId, long anotherUserId)
    {
        try
        {
            var friendship = await context.Friendships
                .FirstOrDefaultAsync(f => f.FromId == myUserId && f.ToId == anotherUserId || f.FromId == anotherUserId && f.ToId == myUserId );
            
            if (friendship == null)
            {
                return "none";
            }

            if (friendship.Status == true)
            {
                return "friends";
            }
            else
            {
                if (friendship.FromId == myUserId)
                {
                    return "requester";
                }
                if (friendship.ToId == myUserId)
                {
                    return "recipient";
                }
            }

            return "none";
        } catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }


}