using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Caching.Interfaces;
using SharedLibrary.Caching.Services;
using StackExchange.Redis;

namespace SharedLibrary.Caching.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRedisCaching(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("Redis");

        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(connectionString!));

        services.AddScoped<ICacheService, RedisCacheService>();

        return services;
    }
}