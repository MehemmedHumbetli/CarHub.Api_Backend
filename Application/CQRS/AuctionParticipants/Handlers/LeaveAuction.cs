using Application.CQRS.AuctionParticipants.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.AuctionParticipants.Handlers;

public class LeaveAuction
{
    public class LeaveAuctionCommand : IRequest<Result<LeaveAuctionDto>>
    {
        public int AuctionId { get; set; }
        public int UserId { get; set; }
    }

    public sealed class LeaveAuctionCommandHandler : IRequestHandler<LeaveAuctionCommand, Result<LeaveAuctionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeaveAuctionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<LeaveAuctionDto>> Handle(LeaveAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = await _unitOfWork.AuctionRepository.GetByIdAsync(request.AuctionId);
            var userInfo = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);

            if (auction == null || !auction.IsActive)
            {
                return new Result<LeaveAuctionDto>() { Errors = ["Auction not found or user is not join."], IsSuccess = false };
            }

            await _unitOfWork.ParticipantRepository.LeaveAuction(request.AuctionId,request.UserId);
            await _unitOfWork.SaveChangeAsync();

            var leaveAuctionDto = new LeaveAuctionDto
            {
                Message = $" {userInfo.Name} {userInfo.Surname} leaved from auction."
            };

            return new Result<LeaveAuctionDto>
            {
                Data = leaveAuctionDto,
                Errors = [],
                IsSuccess = true
            };

        }
    }
}

