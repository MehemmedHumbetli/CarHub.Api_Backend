using Application.CQRS.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Handlers;

public class Update
{
    public record struct Command : IRequest<Result<UpdateDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<Command, Result<UpdateDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<UpdateDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
            if (currentCategory == null) throw new BadRequestException($"User does not exist with id {request.Id}");

            currentCategory.Name = request.Name;
            currentCategory.Description = request.Description;
            //currentUser.UpdatedBy = 1;

            _unitOfWork.CategoryRepository.Update(currentCategory);

            var response = _mapper.Map<UpdateDto>(currentCategory);

            return new Result<UpdateDto>()
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
