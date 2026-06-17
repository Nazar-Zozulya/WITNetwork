

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;

public class ChatService(NetworkDBContext context, IMapper mapper) : IChatService
{
    public async Task<Chat> GetChat(Guid userId, Guid anotherUserId) {
        var findChat = await context.Chats
            .Include(c => c.Admin)
            .Include(c => c.Users)
            .Include(c => c.Messages)
                .ThenInclude(m => m.Sender)
            .Include(c => c.Messages)
                .ThenInclude(m => m.Readers)
            .Include(c => c.Avatar)
            .FirstOrDefaultAsync(c =>
                !c.IsGroup &&
                (
                    (
                        c.AdminId == anotherUserId &&
                        c.Users.Any(u => u.Id == userId)
                    )
                    ||
                    (
                        c.AdminId == userId &&
                        c.Users.Any(u => u.Id == anotherUserId)
                    )
                )
            );

        if ( findChat != null )
        {
            return findChat;
        }

        var admin = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        var anotherUser = await context.Users.FirstOrDefaultAsync(u => u.Id == anotherUserId);

        var createChat = context.Chats.Add(new Chat { AdminId = userId, Admin = admin,  IsGroup = false, Users = [
            admin,
            anotherUser
        ]  });

        var newChat = await context.Chats
            .Include(c => c.Admin)
            .Include(c => c.Users)
            .Include(c => c.Messages)
                .ThenInclude(m => m.Sender)
            .Include(c => c.Messages)
                .ThenInclude(m => m.Readers)
            .Include(c => c.Avatar)
            .FirstOrDefaultAsync(c =>
                !c.IsGroup &&
                (
                    (
                        c.AdminId == userId &&
                        c.Users.Any(u => u.Id == anotherUserId)
                    )
                    
                )
            );
            
        return newChat;
    }

    public async Task<IEnumerable<Chat>> GetIndividualChats(Guid userId)
    {
        var allChats = await context.Chats
            .Where(c =>
                !c.IsGroup &&
                c.Users.Any(u => u.Id == userId)
            )
            .Include(c => c.Users)
            .Include(c => c.Messages
                .OrderByDescending(m => m.CreatedAt)
                .Take(1))
                .ThenInclude(m => m.Readers)
            .ToListAsync();
        return allChats;
    }
}