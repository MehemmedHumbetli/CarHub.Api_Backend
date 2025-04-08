using Application.CQRS.Cart.ResponseDtos;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Carts.Handlers;


public class GetCartWithLinesByUserId
{
    public class GetCartWithLinesByUserIdCommand : IRequest<Result<GetCartWithLinesByUserIdDto>>
    {
        public int UserId { get; set; }
    }

    public class Handler : IRequestHandler<GetCartWithLinesByUserIdCommand, Result<GetCartWithLinesByUserIdDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetCartWithLinesByUserIdDto>> Handle(GetCartWithLinesByUserIdCommand request, CancellationToken cancellationToken)
        {
           
            var cart = await _unitOfWork.CartRepository.GetCartWithLinesAsync(request.UserId);

            if (cart == null)
            {
                return new Result<GetCartWithLinesByUserIdDto>()
                {
                    Errors = { "User's cart not found" },
                    IsSuccess = false
                };
            }

           
            var response = new GetCartWithLinesByUserIdDto
            {
                UserId = cart.UserId,
                CartLines = cart.CartLines.Select(cl => new Domain.Entities.CartLine
                {
                    Id = cl.Id,
                    ProductId = cl.ProductId,
                    Quantity = cl.Quantity,
                    UnitPrice = cl.UnitPrice
                }).ToList()
            };

            return new Result<GetCartWithLinesByUserIdDto>
            {
                Data = response,
                Errors = new List<string>(),
                IsSuccess = true
            };
        }
    }
}

