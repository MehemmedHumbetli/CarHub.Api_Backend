using Application.CQRS.Order.ResponseDtos;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Order.Commands;

public class UpdateOrderStatusCommand : IRequest<Result<Unit>>
{
    public OrderDto Order { get; set; }

    public class Handler : IRequestHandler<UpdateOrderStatusCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var isUpdated = await _unitOfWork.OrderRepository.UpdateOrderStatusAsync(
                request.Order.Id,
                request.Order.Status
            );

            if (!isUpdated)
            {
                return new Result<Unit>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Sifariş tapılmadı və ya silinmişdir." }
                };
            }

            await _unitOfWork.CompleteAsync();

            return new Result<Unit>
            {
                IsSuccess = true,
                Data = Unit.Value
            };
        }
    }
}
