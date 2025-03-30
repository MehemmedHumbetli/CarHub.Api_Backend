using Application.CQRS.Cars.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class GetByYear
{
    public record  struct GetByYearQuery(int minYear, int maxYear) : IRequest<Result<List<GetByYearDto>>>;

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByYearQuery, Result<List<GetByYearDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<GetByYearDto>>> Handle(GetByYearQuery request, CancellationToken cancellationToken)
        {
            var minYear = request.minYear < 0 ? 0 : request.minYear;
            var maxYear = request.maxYear < 0 ? 0 : request.maxYear;
            var cars = (await _unitOfWork.CarRepository.GetByYearAsync(minYear, maxYear)).ToList();
            if (!cars.Any())
                return new Result<List<GetByYearDto>>
                {
                    Data = [],
                    Errors = ["No cars found for the given year range"],
                    IsSuccess = false
                };
            var response = _mapper.Map<List<GetByYearDto>>(cars);
            return new Result<List<GetByYearDto>>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
