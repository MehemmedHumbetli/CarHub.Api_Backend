using Application.CQRS.Cars.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class GetByColor
{
    public record struct GetByColorQuery(string Color) : IRequest<Result<List<GetByColorDto>>> { }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByColorQuery, Result<List<GetByColorDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<GetByColorDto>>> Handle(GetByColorQuery request, CancellationToken cancellationToken)
        {
            var cars = (await _unitOfWork.CarRepository.GetByColorAsync(request.Color)).ToList();
            if (!cars.Any())
                return new Result<List<GetByColorDto>>
                {
                    Data = [],
                    Errors = ["No cars found for the given color"],
                    IsSuccess = false
                };
            var response = _mapper.Map<List<GetByColorDto>>(cars);
            return new Result<List<GetByColorDto>>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
