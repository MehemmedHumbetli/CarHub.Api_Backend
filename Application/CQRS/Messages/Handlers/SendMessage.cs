
using Application.CQRS.SignalR.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using DAL.SqlServer.Context;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using System.Reflection;

namespace Application.CQRS.SignalR.Handlers
{
    public class SendMessage
    {
        public class SendMessageCommand : IRequest<Result<ChatMessageDto>>
        {
            public int SenderId { get; set; }
            public int ReceiverId { get; set; }
            public string Text { get; set; }
        }

        public sealed class Handler : IRequestHandler<SendMessageCommand, Result<ChatMessageDto>>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly AppDbContext _context;
            public Handler(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<ChatMessageDto>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
            {
                var message = new ChatMessage
                {
                    SenderId = request.SenderId,
                    ReceiverId = request.ReceiverId,
                    Text = request.Text,
                    SentAt = DateTime.UtcNow 
                };

                var notification = new Notification
                {
                    UserId = request.SenderId,
                    Title = $"{request.ReceiverId}",
                    Message = request.Text
                };

                await _unitOfWork.ChatMessageRepository.AddAsync(message);
                _context.Notifications.Add(notification);
                await _unitOfWork.SaveChangeAsync();


                var response = _mapper.Map<ChatMessageDto>(message);

                return new Result<ChatMessageDto>
                {
                    Data = response,
                    Errors = new List<string>(), 
                    IsSuccess = true
                };
            }
        }

    }
}

