using Application.CQRS.Cars.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class Add
{
    public class AddCommand : IRequest<Result<AddDto>>
    {
        public int UserId { get; set; }
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

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddCommand, Result<AddDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<AddDto>> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            
            var car = _mapper.Map<Car>(request);
            car.UserId = request.UserId;
            car.Brand = request.Brand;
            car.Model = request.Model;
            car.Year = request.Year;
            car.Price = request.Price;
            car.Fuel = request.Fuel;
            car.Transmission = request.Transmission;
            car.Miles = request.Miles;
            car.CarImagePaths = request.CarImagePaths;
            car.Body = request.Body;
            car.Color = request.Color;
            car.VIN = request.VIN;
            car.Text = request.Text;

            car.CreatedBy = car.UserId;
            await _unitOfWork.CarRepository.AddAsync(car);

            var response = _mapper.Map<AddDto>(car);

            return new Result<AddDto>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
