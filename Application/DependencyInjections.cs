using Application.AutoMapper;
using Application.Services.BackgroundServices; 
using AutoMapper; 
using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.PipelineBehaviour;

namespace Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        #region Automapper config

        MapperConfiguration mapperConfig = new(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        #endregion

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddHostedService<DeleteUserBackgroundService>();

        return services;
    }
}
