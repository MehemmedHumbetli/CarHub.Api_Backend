using Application.CQRS.Cars.ResponseDtos;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class CarGetById
{
    public class CarGetByIdCommand : IRequest<Result<CarGetByIdDto>>
    {
        public int Id { get; set; }
    }

    public sealed class Handler : IRequestHandler<CarGetByIdCommand, Result<CarGetByIdDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CarGetByIdDto>> Handle(CarGetByIdCommand request, CancellationToken cancellationToken)
        {
            var currentCar = await _unitOfWork.CarRepository.GetByIdAsync(request.Id);

            if (currentCar == null)
            {
                return new Result<CarGetByIdDto>() { Errors = ["Car not found"], IsSuccess = false };
            }

            CarGetByIdDto response = new()
            {
                Id = currentCar.Id,
                Brand = currentCar.Brand,
                BrandImagePath = currentCar.BrandImagePath,
                Model = currentCar.Model,
                Year = currentCar.Year,
                Price = currentCar.Price,
                Fuel = currentCar.Fuel.ToString(),
                Transmission = currentCar.Transmission.ToString(),
                Miles = currentCar.Miles,
                CarImagePaths = currentCar.CarImagePaths,
                Color = currentCar.Color,
                Body = currentCar.Body.ToString(),
                BodyTypeImage = currentCar.BodyTypeImage,
                VIN = currentCar.VIN,
                Text = currentCar.Text,
                FavoritedByUsers = currentCar.FavoritedByUsers,
                //.Select(f => f.UserId).ToList()

            };

            return new Result<CarGetByIdDto>() { Data = response, Errors = [], IsSuccess = true };
        }
    }
}
