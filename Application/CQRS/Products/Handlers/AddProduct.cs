using Application.CQRS.Products.ResponsesDto;
using Application.Services;
using AutoMapper;
using Common.GlobalResponses;
using Common.GlobalResponses.Generics;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Repository.Common;

namespace Application.CQRS.Products.Handlers;

public class AddProduct
{
    public class AddProductCommand : IRequest<Result<AddProductDto>>
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string Description { get; set; }
        public List<IFormFile> ImagePath { get; set; } 
        public int CategoryId { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddProductCommand, Result<AddProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<AddProductDto>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var imagePaths = new List<string>();

            if (request.ImagePath != null && request.ImagePath.Any())
            {
                foreach (var image in request.ImagePath)
                {
                    var savedImagePath = await ImageService.SaveImageAsync(image, "uploads/products");
                    imagePaths.Add(savedImagePath);
                }
            }

            var newProduct = new Product
            {
                Name = request.Name,
                UnitPrice = request.UnitPrice,
                UnitsInStock = request.UnitsInStock,
                Description = request.Description,
                ImagePath = imagePaths,
                CategoryId = request.CategoryId
            };

            await _unitOfWork.ProductRepository.AddAsync(newProduct);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<AddProductDto>(newProduct);

            return new Result<AddProductDto>
            {
                Data = response,
                Errors = new List<string>(),
                IsSuccess = true
            };
        }
    }
}
