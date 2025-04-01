using Application.CQRS.Cars.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class GetByPrice
{
    public record struct GetByPriceQuery(decimal MaxPrice) : IRequest<Result<List<GetByPriceDto>>>;

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetByPriceQuery, Result<List<GetByPriceDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<GetByPriceDto>>> Handle(GetByPriceQuery request, CancellationToken cancellationToken)
        {
            var maxPrice = request.MaxPrice < 0 ? 0 : request.MaxPrice; 

            var cars = (await _unitOfWork.CarRepository.GetByPriceAsync(0, maxPrice)).ToList();
            if (!cars.Any())
                return new Result<List<GetByPriceDto>>
                {
                    Data = [],
                    Errors = ["No cars found for the given price range"],
                    IsSuccess = false
                };

            var response = _mapper.Map<List<GetByPriceDto>>(cars);

            return new Result<List<GetByPriceDto>>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
