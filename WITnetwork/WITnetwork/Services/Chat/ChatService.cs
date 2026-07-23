

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;
using WITnetwork.Dtos;
using WITnetwork.Models;

namespace WITnetwork.Services;

public class ChatService(NetworkDBContext context, IMapper mapper) : IChatService
{
    public async Task<ChatResponseDto> GetChat(long userId, long anotherUserId) {
        var findChat = await context.Chats
            .Include(c => c.Admin)
            .Include(c => c.Users)
            // .Include(c => c.Messages)
                // .ThenInclude(m => m.Sender)
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
            var mappedFindChat = mapper.Map<ChatResponseDto>(findChat);

            return mappedFindChat;
        }

        var admin = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (admin == null)
        {
            throw new Exception("admin not found");
        }

        var anotherUser = await context.Users.FirstOrDefaultAsync(u => u.Id == anotherUserId);

        if (anotherUser == null)
        {
            throw new Exception("another user not found");
        }


        var createChat = context.Chats.Add(new Chat { AdminId = userId, Admin = admin,  IsGroup = false, Users = [
            admin,
            anotherUser
        ]  });
        
        await context.SaveChangesAsync();

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

        
        if (newChat == null)
        {
            throw new Exception("createing chat error");
        }
            
        var mappedNewChat = mapper.Map<ChatResponseDto>(newChat);

        return mappedNewChat;
    }

    public async Task<IEnumerable<ChatResponseDto>> GetIndividualChats(long userId, int page, int size)
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
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
            
        var mappedAllChats = mapper.Map<IEnumerable<ChatResponseDto>>(allChats);

        return mappedAllChats;
    }
    
    public async Task<ChatResponseDto> AddUsersToChatAsync(long chatId, long adminId, List<long> userIds)
    {
        var chat = await context.Chats
            .Include(c => c.Users)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == chatId && c.AdminId == adminId);

        if (chat == null)
        {
            throw new Exception("Chat not found or you are not the admin.");
        }

        var usersToAdd = await context.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();

        foreach (var user in usersToAdd)
        {
            if (!chat.Users.Any(u => u.Id == user.Id))
            {
                chat.Users.Add(user);
            }
        }

        await context.SaveChangesAsync();

        var mappedChat = mapper.Map<ChatResponseDto>(chat);

        return mappedChat;
    }

    public async Task<IEnumerable<MessageDto>> GetMessagesFromChat (long chatId, int page, int size)
    {
        try
        {
            var chat = await context.Chats
                .Include(c => c.Messages
                    .OrderByDescending(m => m.CreatedAt)
                    .Skip((page - 1) * size)
                    .Take(size))
                    .ThenInclude(m => m.Readers)
                .FirstOrDefaultAsync(c => c.Id == chatId);
            
            if (chat == null) throw new Exception("chat not found");

            var mappedMessages = mapper.Map<IEnumerable<MessageDto>>(chat.Messages.OrderBy(m => m.CreatedAt));

            return mappedMessages;
        } 
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
}