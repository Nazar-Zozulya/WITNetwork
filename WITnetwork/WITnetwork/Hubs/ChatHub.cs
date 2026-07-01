

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WITnetwork.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

[Authorize]
public class ChatHub(NetworkDBContext context, IMapper mapper, IChatService chatService) : Hub
{
    public void JoinChat(Guid chatId)
    {
        Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chatId}");
        System.Console.WriteLine(123123);
    }

    public async Task LeaveChat(Guid chatId)
    {
        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            $"chat_{chatId}"
        );

        await Clients.OthersInGroup($"chat_{chatId}")
            .SendAsync("chat:user_left", Context.UserIdentifier);
    }

    public async Task<ChatResponseDto> AddUsersToChat(AddUsersRequestDto request)
    {
        var adminId = Guid.Parse(Context.UserIdentifier!);

        var updatedChat = await chatService.AddUsersToChatAsync(
            request.ChatId,
            adminId,
            request.UserIds
        );

        var chatDto = mapper.Map<ChatResponseDto>(updatedChat);

        await Clients.OthersInGroup($"chat_{request.ChatId}")
            .SendAsync("chat:updated", chatDto);

        return chatDto;
    }



    
    public async void SendMessage(Guid chatId, string text)
    {
        var senderId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (senderId == null) return;

        var sender = await context.Users
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(senderId));

        if (sender == null) return;

        var NewMessage = context.Messages.Add(new Message { Text = text, Sender = sender, SenderId = sender.Id });

        await context.SaveChangesAsync();

        var messageDto = mapper.Map<MessageDto>(NewMessage.Entity);

        await Clients.Group($"chat_{chatId.ToString()}").SendAsync("message:new", messageDto);
    }

    public async void SeeMessage(Guid MessageId)
    {
        var readerId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (readerId == null) return;

        var reader = await context.Users
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(readerId));

        if (reader == null) return;

        var message = await context.Messages.Include(m => m.Readers).FirstOrDefaultAsync(message => message.Id == MessageId);

        if (!message.Readers.Any(u => u.Id == reader.Id))
        {
            message.Readers.Add(reader);
            await context.SaveChangesAsync();
        }

        if (message == null) return;

        await Clients.Group($"chat_{message.ChatId.ToString()}").SendAsync("message:saw", message);
    }
}