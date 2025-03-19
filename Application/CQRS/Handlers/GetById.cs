using Application.CQRS.ResponseDtos;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Handlers;

public class GetById
{
    public class Query : IRequest<Result<GetByIdDto>>
    {
        public int Id { get; set; }
    }

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, Result<GetByIdDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<GetByIdDto>> Handle(Query request, CancellationToken cancellationToken)
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
