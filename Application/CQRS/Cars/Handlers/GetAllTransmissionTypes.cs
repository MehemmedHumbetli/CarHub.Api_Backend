using Application.CQRS.Cars.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using Domain.Enums;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class GetAllTransmissionTypes
{
    public class GetAllTransmissionTypesQuery : IRequest<Result<List<GetAllBodyTypesDto>>>
    {
    }
    public sealed class Handler : IRequestHandler<GetAllTransmissionTypesQuery, Result<List<GetAllBodyTypesDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllBodyTypesDto>>> Handle(GetAllTransmissionTypesQuery request, CancellationToken cancellationToken)
        {
            var enumValues = Enum.GetValues(typeof(TransmissionTypes))
                .Cast<TransmissionTypes>()
                .Select(bt => new GetAllBodyTypesDto
                {
                    Id = (int)bt,
                    Name = bt.ToString()
                })
                .ToList();


            if (!enumValues.Any())
                return new Result<List<GetAllBodyTypesDto>>
                {
                    Data = [],
                    Errors = ["No body types found"],
                    IsSuccess = false
                };

            return new Result<List<GetAllBodyTypesDto>>
            {
                Data = enumValues,
                Errors = [],
                IsSuccess = true
            };
        }

    }
}

