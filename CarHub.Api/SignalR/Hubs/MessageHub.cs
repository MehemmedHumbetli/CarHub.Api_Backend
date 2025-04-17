using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs;

public class MessageHub : Hub
{
    public async Task SendMessage(string receiverUserId, string message)
    {
        var senderUserId = Context.UserIdentifier;

        await Clients.User(receiverUserId).SendAsync("ReceiveMessage", senderUserId, message);
    }
}
