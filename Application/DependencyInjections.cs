using Application.AutoMapper;

using Application.PipelineBehaviour;
using Application.Services;
using AutoMapper;
using DAL.SqlServer.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using System.Reflection;
using static Application.CQRS.Telegram.Handlers.SendTelegramMessage;



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
        services.AddScoped<IChatMessageRepository, SqlChatMessageRepository>();

        services.AddScoped<ITelegramService, TelegramService>();
        services.AddMediatR(typeof(SendTelegramMessageCommandHandler).Assembly);
        return services;

        return services;
    }
}

