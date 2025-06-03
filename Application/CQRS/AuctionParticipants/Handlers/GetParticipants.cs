using Application.CQRS.AuctionParticipants.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;
using static Application.CQRS.AuctionParticipants.Handlers.LeaveAuction;

namespace Application.CQRS.AuctionParticipants.Handlers;

public class GetParticipants
{
    public class GetParticipantsCommand : IRequest<Result<List<JoinAuctionDto>>>
    {
        public int AuctionId { get; set; }
    }

    public sealed class GetParticipantsCommandHandler : IRequestHandler<GetParticipantsCommand, Result<List<JoinAuctionDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetParticipantsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<JoinAuctionDto>>> Handle(GetParticipantsCommand request, CancellationToken cancellationToken)
        {
            var auction = await _unitOfWork.AuctionRepository.GetByIdAsync(request.AuctionId);

            if (auction == null || !auction.IsActive)
            {
                return new Result<List<JoinAuctionDto>>() { Errors = ["Auction not found or auction is not active."], IsSuccess = false };
            }

            var GetParticipants = await _unitOfWork.ParticipantRepository.GetParticipants(request.AuctionId);

            var getParticipantsAuctionDto = _mapper.Map<List<JoinAuctionDto>>(GetParticipants);

            return new Result<List<JoinAuctionDto>>
            {
                Data = getParticipantsAuctionDto,
                Errors = [],
                IsSuccess = true
            };

        }
    }
}
