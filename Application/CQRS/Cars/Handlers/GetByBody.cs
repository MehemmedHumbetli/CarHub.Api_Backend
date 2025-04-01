using Application.CQRS.Cars.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using Domain.Enums;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class GetByBody
{
    public record struct GetByBodyQuery(BodyTypes Body) : IRequest<Result<List<GetByBodyDto>>> { }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByBodyQuery, Result<List<GetByBodyDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<GetByBodyDto>>> Handle(GetByBodyQuery request, CancellationToken cancellationToken)
        {
            var cars = (await _unitOfWork.CarRepository.GetByBodyAsync(request.Body)).ToList();

            if (!cars.Any())
                return new Result<List<GetByBodyDto>>
                {
                    Data = [],
                    Errors = ["No cars found for the given body type"],
                    IsSuccess = false
                };

            var response = _mapper.Map<List<GetByBodyDto>>(cars);

            return new Result<List<GetByBodyDto>>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
