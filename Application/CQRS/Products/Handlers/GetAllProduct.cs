using Application.CQRS.Products.ResponsesDto;
using AutoMapper;
using Common.GlobalResponses.Generich;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Products.Handlers;

public class GetAll
{
    public record struct GetAllCategoryQuery : IRequest<Result<List<GetAllProductDto>>> { }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllCategoryQuery, Result<List<GetAllProductDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<GetAllProductDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var product = _unitOfWork.ProductRepository.GetAll();
            if (product == null || !product.Any())
                return new Result<List<GetAllProductDto>>
                {
                    Data = [],
                    Errors = ["No cars found"],
                    IsSuccess = false
                };

            var response = _mapper.Map<List<GetAllProductDto>>(product);

            return new Result<List<GetAllProductDto>>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
