using Application.CQRS.Products.ResponsesDto;
using Application.Services;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using MediatR;
using Microsoft.AspNetCore.Http;
using Repository.Common;

public class UpdateProduct
{
    public class UpdateProductCommand : IRequest<Result<UpdateProductDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string Description { get; set; }

        public IFormFile? NewImage { get; set; }

        public int? IndexToUpdate { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateProductCommand, Result<UpdateProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<UpdateProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
            if (product == null)
                throw new BadRequestException($"Product not found with id {request.Id}");

            var imagePaths = product.ImagePath ?? new List<string>();

            if (request.IndexToUpdate is not null && request.NewImage != null)
            {
                var index = request.IndexToUpdate.Value;

                if (index >= 0 && index < imagePaths.Count)
                {
                    var newImagePath = await ImageService.SaveImageAsync(request.NewImage, "uploads/products");

                    imagePaths[index] = newImagePath;
                }
                else
                {
                    throw new BadRequestException($"Image index {index} is out of range.");
                }
            }

            product.Name = request.Name;
            product.CategoryId = request.CategoryId;
            product.UnitPrice = request.UnitPrice;
            product.UnitsInStock = request.UnitsInStock;
            product.Description = request.Description;
            product.ImagePath = imagePaths;

            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<UpdateProductDto>(product);

            return new Result<UpdateProductDto>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
