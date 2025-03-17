using Application.CQRS.Users.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Users.Handlers;

public class GetAll
{
    public record struct GetAllCarsQuery : IRequest<Result<List<GetAllDto>>> { }

    public sealed class Handler : IRequestHandler<GetAllCarsQuery, Result<List<GetAllDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllDto>>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = _unitOfWork.CarRepository.GetAll();
            if (cars == null || !cars.Any())
                return new Result<List<GetAllDto>>
                {
                    Data = [],
                    Errors = ["No cars found"],
                    IsSuccess = false
                };

            var response = _mapper.Map<List<GetAllDto>>(cars);

            return new Result<List<GetAllDto>>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
