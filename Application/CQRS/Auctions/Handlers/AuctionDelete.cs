using Application.CQRS.Auctions.ResponseDtos;
using Application.CQRS.Cars.ResponseDtos;
using Application.Services;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Auctions.Handler;

public class AuctionDelete
{
    public class DeleteAuctionCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
        public string MessageReason { get; set; }
        public int userId { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, INotificationService notificationService) : IRequestHandler<DeleteAuctionCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly INotificationService _notificationService = notificationService;
        public async Task<Result<Unit>> Handle(DeleteAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = await _unitOfWork.AuctionRepository.DeleteAsync(request.Id);
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.userId);

            if (auction == null)
            {
                return new Result<Unit>() { Errors = ["Auction not found"], IsSuccess = false };
            }

            //await _unitOfWork.SaveChangeAsync();
            await _notificationService.SendAuctionStoppedNotificationAsync(request.Id, request.MessageReason, user);
            return new Result<Unit>
            {
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
