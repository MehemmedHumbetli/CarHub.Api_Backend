using Application.CQRS.Cars.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class GetByFuel
{
    public record struct GetByFuelQuery(FuelTypes Fuel) : IRequest<Result<List<GetByFuelDto>>> { }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByFuelQuery, Result<List<GetByFuelDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<GetByFuelDto>>> Handle(GetByFuelQuery request, CancellationToken cancellationToken)
        {
            var cars = (await _unitOfWork.CarRepository.GetByFuelAsync(request.Fuel)).ToList();

            if (!cars.Any())
                return new Result<List<GetByFuelDto>>
                {
                    Data = [],
                    Errors = ["No cars found for the given Fuel type"],
                    IsSuccess = false
                };

            var response = _mapper.Map<List<GetByFuelDto>>(cars);

            return new Result<List<GetByFuelDto>>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
