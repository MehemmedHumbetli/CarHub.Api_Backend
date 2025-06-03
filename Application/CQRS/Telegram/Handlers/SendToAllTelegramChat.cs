using Application.Services;
using MediatR;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Telegram.Handlers
{
    public class SendToAllTelegramChat
    {
        public class SendToAllTelegramChatsCommand : IRequest<bool>
        {
            public string Message { get; set; }

            public SendToAllTelegramChatsCommand(string message)
            {
                Message = message;
            }
        }

        public class SendToAllTelegramChatsCommandHandler : IRequestHandler<SendToAllTelegramChatsCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ITelegramService _telegramService;

            public SendToAllTelegramChatsCommandHandler(IUnitOfWork unitOfWork, ITelegramService telegramService)
            {
                _unitOfWork = unitOfWork;
                _telegramService = telegramService;
            }

            public async Task<bool> Handle(SendToAllTelegramChatsCommand request, CancellationToken cancellationToken)
            {
                var chatIds = await _unitOfWork.TelegramChatRepository.GetAllChatIdsAsync();

                foreach (var chatId in chatIds)
                {
                    await _telegramService.SendMessageAsync(chatId, request.Message);
                }

                return true;
            }
        }
    }
}
