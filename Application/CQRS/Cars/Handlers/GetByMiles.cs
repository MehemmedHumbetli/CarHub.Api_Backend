using Application.CQRS.Cars.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class GetByMiles
{
    public record struct GetByMilesQuery(decimal minMiles, decimal maxMiles) : IRequest<Result<List<GetByMilesDto>>>;

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByMilesQuery, Result<List<GetByMilesDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<GetByMilesDto>>> Handle(GetByMilesQuery request, CancellationToken cancellationToken)
        {
            var minMiles = request.minMiles < 0 ? 0 : request.minMiles;
            var maxMiles = request.maxMiles < 0 ? 0 : request.maxMiles;
            var cars = (await _unitOfWork.CarRepository.GetByMilesAsync(minMiles, maxMiles)).ToList();
            if (!cars.Any())
                return new Result<List<GetByMilesDto>>
                {
                    Data = [],
                    Errors = ["No cars found for the given miles range"],
                    IsSuccess = false
                };
            var response = _mapper.Map<List<GetByMilesDto>>(cars);
            return new Result<List<GetByMilesDto>>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
