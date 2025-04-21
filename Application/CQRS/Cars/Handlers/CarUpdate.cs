using Application.CQRS.Cars.ResponseDtos;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class CarUpdate
{
    public record struct UpdateCarCommand : IRequest<Result<CarUpdateDto>>
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public FuelTypes Fuel { get; set; }
        public TransmissionTypes Transmission { get; set; }
        public double Miles { get; set; }
        public List<CarImage> CarImagePaths { get; set; }
        public BodyTypes Body { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public string Text { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateCarCommand, Result<CarUpdateDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CarUpdateDto>> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var currentCar = await _unitOfWork.CarRepository.GetByIdAsync(request.CarId);
            if (currentCar == null) throw new BadRequestException($"Car does not exist with id {request.CarId}");


            currentCar.Brand = request.Brand;
            currentCar.Model = request.Model;
            currentCar.Year = request.Year;
            currentCar.Price = request.Price;
            currentCar.Fuel = request.Fuel;
            currentCar.Transmission = request.Transmission;
            currentCar.Miles = request.Miles;
            currentCar.Body = request.Body;
            currentCar.Color = request.Color;
            currentCar.VIN = request.VIN;
            currentCar.Text = request.Text;
            currentCar.UpdatedBy = currentCar.UserId;

            if (request.CarImagePaths != null && request.CarImagePaths.Any())
            {
                currentCar.CarImagePaths = request.CarImagePaths;
            }

            _unitOfWork.CarRepository.Update(currentCar);

            var response = _mapper.Map<CarUpdateDto>(currentCar);

            return new Result<CarUpdateDto>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }

}
