using Application.CQRS.SignalR.ResponseDtos;
using AutoMapper;
using MediatR;
using Repository.Repositories;

namespace Application.CQRS.Messages.Handlers;

public class GetUserMessages
{
    public class GetUserMessagesCommand : IRequest<List<ChatMessageDto>>
    {
        public int ReceiverId { get; set; }
    }

    public class GetUserMessagesHandler : IRequestHandler<GetUserMessagesCommand, List<ChatMessageDto>>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IMapper _mapper;

        public GetUserMessagesHandler(IChatMessageRepository chatMessageRepository, IMapper mapper)
        {
            _chatMessageRepository = chatMessageRepository;
            _mapper = mapper;
        }

        public async Task<List<ChatMessageDto>> Handle(GetUserMessagesCommand request, CancellationToken cancellationToken)
        {
            var chatMessages = await _chatMessageRepository.GetUserMessages(request.ReceiverId);

            var chatMessageDtos = _mapper.Map<List<ChatMessageDto>>(chatMessages);

            return chatMessageDtos;
        }
    }
}
