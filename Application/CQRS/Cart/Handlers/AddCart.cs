using Application.CQRS.Cart.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cart.Handlers
{
    public class AddCart
    {
        public class AddCartCommand : IRequest<Result<AddCartDto>>
        {
            public int UserId { get; set; }
        }

        public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddCartCommand, Result<AddCartDto>>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IMapper _mapper = mapper;

            public async Task<Result<AddCartDto>> Handle(AddCartCommand request, CancellationToken cancellationToken)
            {
                
                var existingCart = await _unitOfWork.CartRepository.GetUserCartAsync(request.UserId);

                if (existingCart != null)
                {
                    var existingCartDto = _mapper.Map<AddCartDto>(existingCart);

                    return new Result<AddCartDto>
                    {
                        Data = existingCartDto,
                        IsSuccess = true
                    };
                }
                var newCart = new Domain.Entities.Cart
                {
                    UserId = request.UserId,  // Kullanıcının ID'sini ata
                    CartLines = new List<CartLine>()  // Başlangıçta boş bir sepet satırı listesi oluştur
                };

                // Veritabanına yeni sepeti ekle
                await _unitOfWork.CartRepository.AddAsync(newCart);
                await _unitOfWork.CompleteAsync();

                // Sepet verisini DTO'ya dönüştür
                var dto = _mapper.Map<AddCartDto>(newCart);

                return new Result<AddCartDto>
                {
                    Data = dto,
                    Errors = new List<string>(),
                    IsSuccess = true
                };
            }
        }
    }
}
