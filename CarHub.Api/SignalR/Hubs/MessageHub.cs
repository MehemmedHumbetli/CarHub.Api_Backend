

namespace SignalR.Hubs;

using Microsoft.AspNetCore.SignalR;
using MediatR;
using Application.CQRS.SignalR.Handlers;

public class ChatHub : Hub
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task SendMessage(string receiverUserId, string message)
    {
        var senderUserId = Context.UserIdentifier; 

        var sendMessageCommand = new SendMessage.SendMessageCommand
        {
            SenderId = int.Parse(senderUserId), 
            ReceiverId = int.Parse(receiverUserId),
            Text = message
        };

        var result = await _mediator.Send(sendMessageCommand);

        if (result.IsSuccess)
        {
            await Clients.User(receiverUserId).SendAsync("ReceiveMessage", senderUserId, message);
        }
        else
        {
            await Clients.Caller.SendAsync("Error", "Mesaj göndərilərkən xəta baş verdi");
        }
    }
}
