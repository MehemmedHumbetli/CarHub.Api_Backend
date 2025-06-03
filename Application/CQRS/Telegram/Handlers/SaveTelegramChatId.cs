using MediatR;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Telegram.Handlers
{
    public class SaveTelegramChatId
    {
        public class SaveTelegramChatIdCommand : IRequest<bool>
        {
            public string ChatId { get; set; }

            public SaveTelegramChatIdCommand(string chatId)
            {
                ChatId = chatId;
            }
        }

        public class SaveTelegramChatIdCommandHandler : IRequestHandler<SaveTelegramChatIdCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;

            public SaveTelegramChatIdCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<bool> Handle(SaveTelegramChatIdCommand request, CancellationToken cancellationToken)
            {
                await _unitOfWork.TelegramChatRepository.SaveChatIdAsync(request.ChatId);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
        }

      
       
    }
}
