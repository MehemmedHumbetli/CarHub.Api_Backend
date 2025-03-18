using Application.CQRS.ResponseDtos;
using AutoMapper;
using Domain.Entities;
using static Application.CQRS.Handlers.Add;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, AddDto>();
        CreateMap<AddCommand, Category>();

    }
}
