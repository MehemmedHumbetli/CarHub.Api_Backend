using Application.CQRS.Order.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;
using Repository.Repositories;

public class CreateOrder
{
    public class CreateOrderCommand : IRequest<Result<CreateOrderDto>>
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
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

    public sealed class Handler : IRequestHandler<CreateOrderCommand, Result<CreateOrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentService;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper, IPaymentRepository paymentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentService = paymentService;
        }

        public async Task<Result<CreateOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cart = await _unitOfWork.CartRepository.GetCartWithLinesByUserId(request.UserId);

                if (cart == null || !cart.CartLines.Any(cl => !cl.IsDeleted))
                {
                    return new Result<CreateOrderDto>
                    {
                        Data = null,
                        Errors = new List<string> { "The cart is empty or all products have been deleted." },
                        IsSuccess = false
                    };
                }

    
                var order = _mapper.Map<Order>(request);
                order.OrderDate = DateTime.Now;
                order.OrderLines = cart.CartLines
                    .Where(cl => !cl.IsDeleted)
                    .Select(cl => new OrderLine
                    {
                        ProductId = cl.ProductId,
                        Quantity = cl.Quantity,
                        UnitPrice = cl.UnitPrice
                    }).ToList();


                var checkoutUrl = await _paymentService.CreateCheckoutSessionAsync(request.UserId);


                if (string.IsNullOrEmpty(checkoutUrl))
                {
                    return new Result<CreateOrderDto>
                    {
                        Data = null,
                        Errors = new List<string> { "Stripe payment transaction error." },
                        IsSuccess = false
                    };
                }


                await _unitOfWork.OrderRepository.CreateOrderAsync(order);
                await _unitOfWork.SaveChangeAsync();

                cart.CartLines.Clear();
                await _unitOfWork.SaveChangeAsync();

                var dto = _mapper.Map<CreateOrderDto>(order);
                dto.CheckoutUrl = checkoutUrl;

                return new Result<CreateOrderDto>
                {
                    Data = dto,
                    Errors = new List<string>(),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {

                Console.WriteLine("❌ Error: " + ex.Message);
                return new Result<CreateOrderDto>
                {
                    Data = null,
                    Errors = new List<string> { "An error occurred: " + ex.Message },
                    IsSuccess = false
                };
            }
        }
    }
}
