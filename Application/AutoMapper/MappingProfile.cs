using Application.CRQS.ResponsesDto;
using AutoMapper;
using Domain.Entities;
using static Application.CRQS.Handlers.AddProduct;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, AddProductDto>();
        CreateMap<AddProductCommand, Product>();

        CreateMap<Product, GetAllProductDto>();
        CreateMap<GetAllProductDto, Product>();

        CreateMap<Product, UpdateProductDto>();
        CreateMap<UpdateProductDto, Product>();

        CreateMap<Product, GetByNameProductDto>();
        CreateMap<GetByNameProductDto, Product>();

        CreateMap<Product , ProductResponseDto>();
        CreateMap<ProductResponseDto, Product>();



    }
}