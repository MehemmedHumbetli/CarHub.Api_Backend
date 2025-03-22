using Application.CQRS.Products.ResponsesDto;
using Common.GlobalResponses.Generich;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Products.Handlers;

public class GetByNameProduct
{
    public class Query : IRequest<Result<GetByNameProductDto>>
    {
        public string Name { get; set; }
    }

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, Result<GetByNameProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<GetByNameProductDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var currentproduct = await _unitOfWork.ProductRepository.GetByNameAsync(request.Name);
            if (currentproduct == null)
            {
                return new Result<GetByNameProductDto>() { Errors = ["Products tapilmadi"], IsSuccess = true };
            }
            GetByNameProductDto response = new()
            {
                Id = currentproduct.Id,
                Name = currentproduct.Name,
                Description = currentproduct.Description,
                ImagePath = currentproduct.ImagePath,
                UnitPrice = currentproduct.UnitPrice,
                UnitsInStock = currentproduct.UnitsInStock


            };

            return new Result<GetByNameProductDto> { Data = response, Errors = [], IsSuccess = true };
        }
    }
}