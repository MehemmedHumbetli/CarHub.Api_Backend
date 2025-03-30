using Application.CQRS.Cars.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using Domain.Enums;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class GetByTransmission
{
    public record struct GetByTransmissionQuery(TransmissionTypes transmission) : IRequest<Result<List<GetByTransmissionDto>>> { }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByTransmissionQuery, Result<List<GetByTransmissionDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<GetByTransmissionDto>>> Handle(GetByTransmissionQuery request, CancellationToken cancellationToken)
        {
            var cars = (await _unitOfWork.CarRepository.GetByTransmissionAsync(request.transmission)).ToList();
            if (!cars.Any())
                return new Result<List<GetByTransmissionDto>>
                {
                    Data = [],
                    Errors = ["No cars found for the given transmission"],
                    IsSuccess = false
                };
            var response = _mapper.Map<List<GetByTransmissionDto>>(cars);
            return new Result<List<GetByTransmissionDto>>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
