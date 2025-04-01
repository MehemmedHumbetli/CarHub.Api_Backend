using Application.CQRS.Cars.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class GetByModel
{
    public record struct GetByModelQuery(string Model) : IRequest<Result<List<GetByModelDto>>> { }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByModelQuery, Result<List<GetByModelDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<GetByModelDto>>> Handle(GetByModelQuery request, CancellationToken cancellationToken)
        {
            var cars = (await _unitOfWork.CarRepository.GetByModelAsync(request.Model)).ToList();
            if (!cars.Any())
                return new Result<List<GetByModelDto>>
                {
                    Data = [],
                    Errors = ["No cars found for the given model"],
                    IsSuccess = false
                };
            var response = _mapper.Map<List<GetByModelDto>>(cars);
            return new Result<List<GetByModelDto>>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
