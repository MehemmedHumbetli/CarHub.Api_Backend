using Application.CQRS.Users.ResponseDtos;
using AutoMapper;
using Domain.Entities;
using static Application.CQRS.Users.Handlers.Add;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddCommand, Car>().ReverseMap();
        CreateMap<Car, AddDto>();

        //CreateMap<User, UpdateDto>();
        CreateMap<Car, GetAllDto>();
    }
}
