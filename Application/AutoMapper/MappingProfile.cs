using AutoMapper;
using Domain.Entities;
using static Application.CQRS.Categories.Handlers.Add;
//using static System.Runtime.InteropServices.JavaScript.JSType;
using static Application.CQRS.Products.Handlers.AddProduct;
using Application.CQRS.Categories.ResponseDtos;
using Application.CQRS.Products.ResponsesDto;


namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<Category, AddDto>();
        CreateMap<AddCommand, Category>();

        CreateMap<Category, GetAllDto>();
        CreateMap<GetAllDto, Category>();

        CreateMap<Category, UpdateDto>();
        CreateMap<UpdateDto, Category>();

        CreateMap<Category, DeleteDto>();
        CreateMap<DeleteDto, Category>();

        CreateMap<GetCategoriesWithProductsDto, Category>();
        CreateMap<Category, GetCategoriesWithProductsDto>();
        CreateMap<Product, ProductDto>();


        //Product Mapping
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

        CreateMap<Product, GetProductsByPriceRange>();

    }
}

