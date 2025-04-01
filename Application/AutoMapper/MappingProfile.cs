using Application.CQRS.Users.ResponseDtos;
using static Application.CQRS.Users.Handlers.Register;
using static Application.CQRS.Users.Handlers.Update;
using AutoMapper;
using Domain.Entities;
using static Application.CQRS.Cars.Handlers.Add;
using Application.CQRS.Cars.ResponseDtos;
using Application.CQRS.Users.Handlers;

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
        CreateMap<Car, GetUserFavoritesDto>();
        CreateMap<Car, GetUserCarsDto>();


        //Car Mapping
        CreateMap<AddCommand, Car>().ReverseMap();
        CreateMap<Car, AddDto>();
        CreateMap<Car, CarGetAllDto>();
        CreateMap<Car, CarUpdateDto>();
        CreateMap<Car, GetByBodyDto>();
        CreateMap<Car, GetByBrandDto>();
        CreateMap<Car, GetByFuelDto>();
        CreateMap<Car, GetByColorDto>();
        CreateMap<Car, GetByTransmissionDto>();
        CreateMap<Car, GetByModelDto>();
        CreateMap<Car, GetByPriceDto>();
        CreateMap<Car, GetByMilesDto>();
        CreateMap<Car, GetByYearDto>();
    }
}
