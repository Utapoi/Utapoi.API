using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Karaoke.Application.Common.Behaviours;
using Karaoke.Application.Singers.Commands.CreateSinger;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Karaoke.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableDataAnnotationsValidation = true;
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            c.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        return services;
    }
}