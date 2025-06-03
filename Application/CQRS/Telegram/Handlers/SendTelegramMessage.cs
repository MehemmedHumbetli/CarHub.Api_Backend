using Application.Services;
using MediatR;

namespace Application.CQRS.Telegram.Handlers;

public class SendTelegramMessage
{
    public class SendTelegramMessageCommand : IRequest<bool>
    {
        public string ChatId { get; set; }
        public string Message { get; set; }

        public SendTelegramMessageCommand(string chatId, string message)
        {
            ChatId = chatId;
            Message = message;
        }
    }

    public class SendTelegramMessageCommandHandler : IRequestHandler<SendTelegramMessageCommand, bool>
    {
        private readonly ITelegramService _telegramService;

        public SendTelegramMessageCommandHandler(ITelegramService telegramService)
        {
            _telegramService = telegramService;
        }

        public async Task<bool> Handle(SendTelegramMessageCommand request, CancellationToken cancellationToken)
        {
            await _telegramService.SendMessageAsync(request.ChatId, request.Message);
            return true;
        }
    }
}
