using Application.CQRS.Order.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

public class CreateOrder
{
    public class CreateOrderCommand : IRequest<Result<CreateOrderDto>>
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper): IRequestHandler<CreateOrderCommand, Result<CreateOrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CreateOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.CartRepository.GetCartWithLinesByUserId(request.UserId);
            if (cart == null || !cart.CartLines.Any())
            {
                return new Result<CreateOrderDto>
                {
                    Data = null,
                    Errors = new List<string> { "Sepet boşdu." },
                    IsSuccess = false
                };
            }

            var order = _mapper.Map<Order>(request);
            order.OrderDate = DateTime.Now;
            order.OrderLines = cart.CartLines.Select(cartLine => new OrderLine
            {
                ProductId = cartLine.ProductId,
                Quantity = cartLine.Quantity,
                UnitPrice = cartLine.UnitPrice
            }).ToList();

            await _unitOfWork.OrderRepository.CreateOrderAsync(order);
            await _unitOfWork.SaveChangeAsync();

            cart.CartLines.Clear();
            await _unitOfWork.SaveChangeAsync();

            var dto = _mapper.Map<CreateOrderDto>(order);

            return new Result<CreateOrderDto>
            {
                Data = dto,
                Errors = new List<string>(),
                IsSuccess = true
            };
        }
    }
}
