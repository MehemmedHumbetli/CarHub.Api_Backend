using Application.CQRS.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Handlers;



public class GetAll
{
    public record struct GetAllCategoryQuery : IRequest<Result<List<GetAllDto>>> { }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllCategoryQuery, Result<List<GetAllDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<GetAllDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            if (categories == null || !categories.Any())
                return new Result<List<GetAllDto>>
                {
                    Data = [],
                    Errors = ["No cars found"],
                    IsSuccess = false
                };

            var response = _mapper.Map<List<GetAllDto>>(categories);

            return new Result<List<GetAllDto>>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}

