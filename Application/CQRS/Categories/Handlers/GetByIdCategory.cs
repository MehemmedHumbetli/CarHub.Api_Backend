using Application.CQRS.Categories.ResponseDtos;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Categories.Handlers;

public class GetByIdCategory
{
    public class CategoryGetByIdCommand : IRequest<Result<GetByIdDto>>
    {
        public int Id { get; set; }
    }

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<CategoryGetByIdCommand, Result<GetByIdDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<GetByIdDto>> Handle(CategoryGetByIdCommand request, CancellationToken cancellationToken)
        {
            var currentCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
            if (currentCategory == null)
            {
                return new Result<GetByIdDto>() { Errors = ["User tapilmadi"], IsSuccess = true };
            }
            GetByIdDto response = new()
            {
                Id = currentCategory.Id,
                Name = currentCategory.Name,
                Description = currentCategory.Description

            };

            return new Result<GetByIdDto> { Data = response, Errors = [], IsSuccess = true };
        }
    }
}
