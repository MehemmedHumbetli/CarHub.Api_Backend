using Application.CQRS.Users.ResponseDtos;
using static Application.CQRS.Users.Handlers.Register;
using static Application.CQRS.Users.Handlers.Update;
using AutoMapper;
using Domain.Entities;
using static Application.CQRS.Cars.Handlers.Add;
using Application.CQRS.Cars.ResponseDtos;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //User Mapping
        CreateMap<RegisterCommand, User>().ReverseMap();
        CreateMap<User, RegisterDto>();

        CreateMap<User, UpdateDto>();
        CreateMap<User, UserGetAllDto>();
        CreateMap<User, GetByIdDto>();
        
        //Car Mapping
        CreateMap<AddCommand, Car>().ReverseMap();
        CreateMap<Car, AddDto>();

        //CreateMap<User, UpdateDto>();
        CreateMap<Car, CarGetAllDto>();
     }
}
