using Application.CQRS.AuctionParticipants.ResponseDtos;
using Application.CQRS.Auctions.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.AuctionParticipants.Handlers;

public class JoinAuction
{
    public class JoinAuctionCommand : IRequest<Result<JoinAuctionDto>>
    {
        public int AuctionId { get; set; }
        public int UserId { get; set; }
    }

    public sealed class JoinAuctionCommandHandler : IRequestHandler<JoinAuctionCommand, Result<JoinAuctionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public JoinAuctionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<JoinAuctionDto>> Handle(JoinAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = await _unitOfWork.AuctionRepository.GetByIdAsync(request.AuctionId);
            var userInfo = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);

            if (auction == null || !auction.IsActive)
            {
                return new Result<JoinAuctionDto>() { Errors = ["Auction not found or is not active."], IsSuccess = false };
            }

            var participant = new AuctionParticipant
            {
                AuctionId = request.AuctionId,
                UserId = request.UserId,
                JoinedAt = DateTime.UtcNow,
                Message = $"{userInfo.Name} {userInfo.Surname} joined the auction."
            };

            await _unitOfWork.ParticipantRepository.JoinAuction(request.AuctionId, request.UserId);
            await _unitOfWork.SaveChangeAsync();

            var joinAuctionDto = _mapper.Map<JoinAuctionDto>(participant);

            return new Result<JoinAuctionDto>
            {
                Data = joinAuctionDto,
                Errors = [],
                IsSuccess = true
            };
        }

    }
}
