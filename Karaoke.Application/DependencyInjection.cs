using Microsoft.Extensions.DependencyInjection;

namespace Karaoke.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}