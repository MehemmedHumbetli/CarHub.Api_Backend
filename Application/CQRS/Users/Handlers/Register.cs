using Application.CQRS.Users.ResponseDtos;
using Common.GlobalResponses.Generics;
using MediatR;
using AutoMapper;
using Repository.Common;
using Common.Exceptions;
using Domain.Entities;

namespace Application.CQRS.Users.Handlers;

public class Register
{
    public class Command : IRequest<Result<RegisterDto>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<Command, Result<RegisterDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<RegisterDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);
            if (currentUser != null) throw new BadRequestException("User is already exist with provided mail");

            var user = _mapper.Map<User>(request);

            var hashPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);
            user.PasswordHash = hashPassword;
            user.CreatedBy = 1;
            await _unitOfWork.UserRepository.RegisterAsync(user);

            var response = _mapper.Map<RegisterDto>(user);

            return new Result<RegisterDto>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}
