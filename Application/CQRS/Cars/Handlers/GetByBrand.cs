using Application.CQRS.Cars.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class GetByBrand
{
    public record struct GetByBrandQuery(string Brand) : IRequest<Result<List<GetByBrandDto>>> { }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByBrandQuery, Result<List<GetByBrandDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<GetByBrandDto>>> Handle(GetByBrandQuery request, CancellationToken cancellationToken)
        {
            var cars = (await _unitOfWork.CarRepository.GetByBrandAsync(request.Brand)).ToList();

            if (!cars.Any())
                return new Result<List<GetByBrandDto>>
                {
                    Data = [],
                    Errors = ["No cars found for the given brand"],
                    IsSuccess = false
                };

            var response = _mapper.Map<List<GetByBrandDto>>(cars);

            return new Result<List<GetByBrandDto>>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
