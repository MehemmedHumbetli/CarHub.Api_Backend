
using Common.GlobalResponses.Generics;
using Domain.Entities;
using Domain.Enums;
using Application.CQRS.Cars.ResponseDtos;
using MediatR;
using Repository.Common;
using AutoMapper;

namespace Application.CQRS.Cars.Handlers;

public class GetFilteredCars
{
    public class CarGetFilteredCommand : IRequest<Result<List<GetFilteredCarsAsyncDto>>>
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? Fuel { get; set; }
        public string? Transmission { get; set; }
        public double? MinMiles { get; set; }
        public double? MaxMiles { get; set; }
        public string? Body { get; set; }
        public string? Color { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CarGetFilteredCommand, Result<List<GetFilteredCarsAsyncDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<GetFilteredCarsAsyncDto>>> Handle(CarGetFilteredCommand request, CancellationToken cancellationToken)
        {
            Car filter = new()
            {
                Brand = request.Brand,
                Model = request.Model,
                Year = request.MinYear ?? 0, 
                Price = request.MaxPrice ?? 0,
                Fuel = Enum.TryParse<FuelTypes>(request.Fuel, true, out var fuel) ? fuel : 0,
                Transmission = Enum.TryParse<TransmissionTypes>(request.Transmission, true, out var transmission) ? transmission : 0,
                Miles = request.MaxMiles ?? 0,
                Body = Enum.TryParse<BodyTypes>(request.Body, true, out var body) ? body : 0,
                Color = request.Color
            };

            var filteredCars = await _unitOfWork.CarRepository.GetFilteredCarsAsync(filter);

            var response = _mapper.Map<List<GetFilteredCarsAsyncDto>>(filteredCars);

            return new Result<List<GetFilteredCarsAsyncDto>>()
            {
                Data = response,
                IsSuccess = true,
                Errors = []
            };
        }
    }
}

