using Application.CQRS.ResponseDtos;
using Application.Security;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Handlers;

public class Add
{
    public class AddCommand : IRequest<Result<AddDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public sealed class Handler : IRequestHandler<AddCommand, Result<AddDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AddDto>> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            // Yeni kategori objesini map et
            var newCategory = _mapper.Map<Domain.Entities.Category>(request);

            // Kategori ismi zorunlu
            if (string.IsNullOrEmpty(newCategory.Name))
            {
                throw new Exception("Category name is required");
            }

            // Kategoriyi veritabanına ekle
            await _unitOfWork.CategoryRepository.AddAsync(newCategory);

            // Cevabı map et
            var response = _mapper.Map<AddDto>(newCategory);

            // Sonuç dön
            return new Result<AddDto>
            {
                Data = response,
                Errors = new List<string>(),  // Hataları boş gönderiyoruz
                IsSuccess = true
            };
        }
    }
}
