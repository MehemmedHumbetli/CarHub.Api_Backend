
using AutoMapper;
using Domain.Entities;
using static Application.CQRS.Categories.Handlers.AddCategory;
//using static System.Runtime.InteropServices.JavaScript.JSType;
using static Application.CQRS.Products.Handlers.AddProduct;
using Application.CQRS.Categories.ResponseDtos;
using Application.CQRS.Products.ResponsesDto;

﻿using Application.CQRS.Users.ResponseDtos;
using static Application.CQRS.Users.Handlers.Register;
using static Application.CQRS.Users.Handlers.Update;
using static Application.CQRS.Cars.Handlers.Add;
using Application.CQRS.Cars.ResponseDtos;
using Application.CQRS.Users.Handlers;


namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        //Category Mapping
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
