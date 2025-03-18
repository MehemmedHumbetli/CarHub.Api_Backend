using Application.CQRS.ResponseDtos;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Handlers;



public sealed class GetAllHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetAllDto>>> Handle(CancellationToken cancellationToken)
    {
      
        var categories =  _unitOfWork.CategoryRepository.GetAll();

        var response = _mapper.Map<List<GetAllDto>>(categories);

        
        return new Result<List<GetAllDto>>
        {
            Data = response,
            Errors = new List<string>(),  
            IsSuccess = true
        };
    }
}

