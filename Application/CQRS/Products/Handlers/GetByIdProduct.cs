using Application.CQRS.Products.ResponsesDto;
using AutoMapper;
using Common.GlobalResponses.Generich;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Products.Handlers;

public class GetByIdProduct
{
    public class Query : IRequest<Result<GetByIdProductDto>>
    {
        public int Id { get; set; }
    }

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, Result<GetByIdProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<GetByIdProductDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var currentproduct = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
            if (currentproduct == null)
            {
                return new Result<GetByIdProductDto>() { Errors = ["Products tapilmadi"], IsSuccess = true };
            }
            GetByIdProductDto response = new()
            {
                Id = currentproduct.Id,
                Name = currentproduct.Name,
                Description = currentproduct.Description

            };

            return new Result<GetByIdProductDto> { Data = response, Errors = [], IsSuccess = true };
        }
    }
}

