

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WITnetwork.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WITnetwork.Services;
using WITnetwork.Dtos;

namespace WITnetwork.Hubs;

[Authorize]
public class ChatHub(NetworkDBContext context, IMapper mapper, IChatService chatService, IHubContext<GlobalHub> globalHub) : Hub
{
    [HubMethodName("chat:join")]
    public async Task EnterChat(long chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chatId}");
        System.Console.WriteLine(123123);
    }

    [HubMethodName("chat:leave")]
    public async Task LeaveChat(long chatId)
    {
        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            $"chat_{chatId}"
        );

        await Clients.OthersInGroup($"chat_{chatId}")
            .SendAsync("chat:user_left", Context.UserIdentifier);
    }

    // public async Task<ChatResponseDto> AddUsersToChat(AddUsersRequestDto request)
    // {
    //     var adminId = long.Parse(Context.UserIdentifier!);

    //     var updatedChat = await chatService.AddUsersToChatAsync(
    //         request.ChatId,
    //         adminId,
    //         request.UserIds
    //     );

    //     var chatDto = mapper.Map<ChatResponseDto>(updatedChat);

    //     await Clients.OthersInGroup($"chat_{request.ChatId}")
    //         .SendAsync("chat:updated", chatDto);

    //     return chatDto;
    // }



    [HubMethodName("message:send")]
    public async Task SendMessage(SendMessageDto dto)
    {
        var senderId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (senderId == null) return;

        var sender = await context.Users
            .FirstOrDefaultAsync(x => x.Id == long.Parse(senderId));

        if (sender == null) return;

        var NewMessage = context.Messages.Add(new Message 
            { 
                Text = dto.Text, 
                Sender = sender, 
                SenderId = sender.Id,
                ChatId = dto.ChatId
            });

        await context.SaveChangesAsync();



        var messageDto = mapper.Map<MessageDto>(NewMessage.Entity);

        await Clients.Group($"chat_{dto.ChatId}").SendAsync("message:new", messageDto);

        var currentChat = await context.Chats
            .Include(c => c.Users)
            .FirstOrDefaultAsync(c => c.Id == dto.ChatId);

        // if (currentChat == null) return;

        foreach (var user in currentChat.Users)
        {
            if (user.Id != dto.SenderId) 
            await globalHub.Clients.Group($"user_{user.Id}")
                .SendAsync("global-message:new", messageDto);
        }

    }

    [HubMethodName("message:see")]
    public async Task SeeMessage(SeeMessageDto dto)
    {
        var readerId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (readerId == null) return;

        var reader = await context.Users
            .FirstOrDefaultAsync(x => x.Id == dto.ReaderId);

        if (reader == null) return;

        var message = await context.Messages
            .Include(m => m.Readers)
            .FirstOrDefaultAsync(message => message.Id == dto.MessageId);

        // if (!message.Readers.Any(u => u.Id == reader.Id))
        // {
        message.Readers.Add(reader);
        await context.SaveChangesAsync();
        // }

        if (message == null) return;

        await Clients.Group($"chat_{message.ChatId.ToString()}").SendAsync("message:saw", message);
    }
}