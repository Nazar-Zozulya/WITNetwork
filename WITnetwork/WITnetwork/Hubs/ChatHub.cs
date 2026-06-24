

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WITnetwork.Data;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class ChatHub(NetworkDBContext context) : Hub
{
    public void JoinChat(Guid chatId)
    {
        Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chatId.ToString()}");
        System.Console.WriteLine(123123);
    }

    
    public async Task SendMessage(Guid chatId, string text)
    {
        var senderId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (senderId == null) return;

        var sender = await context.Users
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(senderId));

        if (sender == null) return;

        var NewMessage = context.Messages.Add(new Message { Text = text, Sender = sender, SenderId = sender.Id });

        await context.SaveChangesAsync();

        Clients.Group($"chat_{chatId.ToString()}").SendAsync("message:new", NewMessage);
    }

    public async Task SeeMessage(Guid MessageId)
    {
        var readerId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (readerId == null) return;

        var reader = await context.Users
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(readerId));

        if (reader == null) return;

        var message = await context.Messages.FirstOrDefaultAsync(message => message.Id == MessageId);

        if (!message.Readers.Any(u => u.Id == reader.Id))
        {
            message.Readers.Add(reader);
            await context.SaveChangesAsync();
        }

        if (message == null) return;

        Clients.Group($"chat_{message.ChatId.ToString()}").SendAsync("message:saw", message);
    }
}