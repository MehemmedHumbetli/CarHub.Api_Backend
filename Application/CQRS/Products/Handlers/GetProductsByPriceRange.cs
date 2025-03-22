using Application.CQRS.Products.ResponsesDto;
using AutoMapper;
using Common.GlobalResponses.Generich;
using Domain.Entities;
using MediatR;
using Repository.Common;
using Repository.Repositories;

namespace Application.CQRS.Products.Handlers;

public class GetProductsByPriceRange
{
    // Query sınıfı
    public class Query : IRequest<Result<List<GetByNameProductDto>>>
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public Query(decimal minPrice, decimal maxPrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }
    }


    public class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<Query, Result<List<GetByNameProductDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<GetByNameProductDto>>> Handle(Query request, CancellationToken cancellationToken)
        {

            var products = await _unitOfWork.ProductRepository.GetProductsByPriceRange(request.MinPrice, request.MaxPrice);


            if (products == null || !products.Any())
            {

                return new Result<List<GetByNameProductDto>>
                {
                    Errors = new List<string> { "No products found in the given price range" },
                    IsSuccess = false
                };
            }

            var productDtos = _mapper.Map<List<GetByNameProductDto>>(products.ToList());


            return new Result<List<GetByNameProductDto>>
            {
                Data = productDtos,
                Errors = new List<string>(),
                IsSuccess = true
            };
        }
    }
}