using Common.GlobalResponses.Generics;
using DAL.SqlServer.Context;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using System.Reflection;

namespace Application.CQRS.Cars.Handlers;

public class ChangeUser
{
    public class ChangeCarUserCommand : IRequest<Result<Unit>>
    {
        public int ownerId { get; set; }
        public int newUserId { get; set; }
        public int carId { get; set; }
    }

    public class Handler : IRequestHandler<ChangeCarUserCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public Handler(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<Result<Unit>> Handle(ChangeCarUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.CarRepository.ChangeUser(request.ownerId,request.newUserId,request.carId);
            await _unitOfWork.SaveChangeAsync();
            return new Result<Unit> { Errors = [], IsSuccess = true };
        }
    }
}
