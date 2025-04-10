using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

public class RemoveProductFromCart
{
    public class RemoveProductFromCartCommand : IRequest<Result<Unit>>
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveProductFromCartCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<Unit>> Handle(RemoveProductFromCartCommand request, CancellationToken cancellationToken)
        {
            
            await _unitOfWork.CartRepository.RemoveProductFromCartAsync(request.CartId, request.ProductId);
            await _unitOfWork.SaveChangeAsync();
            return new Result<Unit>
            {
                IsSuccess = true,
                Data = Unit.Value
            };
        }
    }
}
