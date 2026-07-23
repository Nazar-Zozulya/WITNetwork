using System.Collections.Concurrent;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;
using WITnetwork.Services;

namespace WITnetwork.Hubs;

[Authorize]
public class GlobalHub(
    NetworkDBContext context,
    IMapper mapper,
    IChatService chatService,
    IFriendshipService friendService) : Hub
{
    private static readonly ConcurrentDictionary<long, HashSet<string>> OnlineUsers = new();

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var claim = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (claim != null)
        {
            long currentUserId = long.Parse(claim);

            if (OnlineUsers.TryGetValue(currentUserId, out var connections))
            {
                lock (connections)
                {
                    connections.Remove(Context.ConnectionId);

                    if (connections.Count == 0)
                    {
                        OnlineUsers.TryRemove(currentUserId, out _);
                    }
                }
            }
        }

        await base.OnDisconnectedAsync(exception);
    }

    [HubMethodName("globalChat:join")]
    public async Task EnterGlobalChat(long userId)
    {
        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            $"user_{userId}"
        );

        OnlineUsers.AddOrUpdate(
            userId,
            _ => new HashSet<string> { Context.ConnectionId },
            (_, connections) =>
            {
                lock (connections)
                {
                    connections.Add(Context.ConnectionId);
                }

                return connections;
            });

        var allChats = await context.Chats
            .Include(c => c.Users)
            .Where(c =>
                !c.IsGroup &&
                c.Users.Any(u => u.Id == userId)
            )
            .ToListAsync();

        if (allChats == null)
        {
            return;
        }

        var friendsIds = allChats
            .SelectMany(chat => chat.Users)
            .Where(user => user.Id != userId)
            .Select(user => user.Id)
            .Distinct();

        foreach (var id in friendsIds)
        {
            if (OnlineUsers.ContainsKey(id))
            {
                await Clients.Group($"user_{id}")
                    .SendAsync("user:active", userId);
            }
        }
    }

    [HubMethodName("globalChat:leave")]
    public async Task LeaveGlobalChat(long userId)
    {
        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            $"user_{userId}"
        );

        if (OnlineUsers.TryGetValue(userId, out var connections))
        {
            lock (connections)
            {
                connections.Remove(Context.ConnectionId);

                if (connections.Count == 0)
                {
                    OnlineUsers.TryRemove(userId, out _);
                }
            }
        }

        var allChats = await context.Chats
            .Include(c => c.Users)
            .Where(c =>
                !c.IsGroup &&
                c.Users.Any(u => u.Id == userId)
            )
            .ToListAsync();

        if (allChats == null)
        {
            return;
        }

        var friendsIds = allChats
            .SelectMany(chat => chat.Users)
            .Where(user => user.Id != userId)
            .Select(user => user.Id)
            .Distinct();

        foreach (var id in friendsIds)
        {
            await Clients.Group($"user_{id}")
                .SendAsync("user:deactive", userId);
        }
    }

    [HubMethodName("user:get-statuses")]
    public async Task GetStatuses(long userId)
    {
         var friendships = await context.Friendships
            .Include(f => f.From)
            .Include(f => f.To)
            .Where(f => f.Status == true && 
                (f.FromId == userId || f.ToId == userId))
            .ToListAsync();


        var allFriends = friendships.Select(f =>
            f.FromId == userId ? f.To : f.From);

        // if (allFriends != null)
        // {
        var usersStatuses = allFriends
            .Select(friend => new
            {
                Id = friend.Id,
                Status = OnlineUsers.ContainsKey(friend.Id)
                    ? "active"
                    : "deactive"
            })
            .ToList();

        await Clients.Caller.SendAsync(
            "user:all-statuses",
            usersStatuses
        );
        // }

    }
}