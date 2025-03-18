using Application.CQRS.Users.ResponseDtos;
using AutoMapper;
using Domain.Entities;
using static Application.CQRS.Users.Handlers.Register;
using static Application.CQRS.Users.Handlers.Update;
namespace Application.AutoMapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterCommand, User>().ReverseMap();
        CreateMap<User, RegisterDto>();

        CreateMap<User, UpdateDto>();
        CreateMap<User, GetAllDto>();
        CreateMap<User, GetByIdDto>();
    }
}
