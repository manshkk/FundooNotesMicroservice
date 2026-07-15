using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Messaging.Configuration;
using SharedLibrary.Messaging.Interfaces;
using SharedLibrary.Messaging.Publishers;

namespace SharedLibrary.Messaging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMqMessaging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<RabbitMqSettings>(
            configuration.GetSection(RabbitMqSettings.SectionName));

        services.AddScoped<IMessagePublisher, RabbitMqPublisher>();

        return services;
    }
}