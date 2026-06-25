using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedLibrary.Messaging.Configuration;
using SharedLibrary.Messaging.Events;
using UserService.Application.Interfaces;

namespace UserService.API.BackgroundServices;

public class UserRegisteredConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly RabbitMqSettings _settings;
    private readonly ILogger<UserRegisteredConsumer> _logger;

    private IConnection? _connection;
    private IModel? _channel;

    public UserRegisteredConsumer(
        IServiceScopeFactory scopeFactory,
        IOptions<RabbitMqSettings> options,
        ILogger<UserRegisteredConsumer> logger)
    {
        _scopeFactory = scopeFactory;
        _settings = options.Value;
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: "fundoonotes.user.registered",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel!);

        consumer.Received += async (sender, eventArgs) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                var user = JsonSerializer.Deserialize<UserRegisteredEvent>(json);

                if (user == null)
                {
                    _channel!.BasicAck(eventArgs.DeliveryTag, false);
                    return;
                }

                using var scope = _scopeFactory.CreateScope();

                var emailService =
                    scope.ServiceProvider.GetRequiredService<IEmailService>();

                await emailService.SendWelcomeEmailAsync(
                    user.Email,
                    user.FirstName);

                _channel!.BasicAck(eventArgs.DeliveryTag, false);

                _logger.LogInformation(
                    "Welcome email sent to {Email}",
                    user.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RabbitMQ Consumer Error");

                _channel!.BasicNack(eventArgs.DeliveryTag, false, true);
            }
        };

        _channel!.BasicConsume(
            queue: "fundoonotes.user.registered",
            autoAck: false,
            consumer: consumer);

        _logger.LogInformation("RabbitMQ consumer is listening...");

        // Keep the background service alive
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();

        _channel?.Dispose();
        _connection?.Dispose();

        base.Dispose();
    }
}