using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SharedLibrary.Messaging.Configuration;
using SharedLibrary.Messaging.Interfaces;

namespace SharedLibrary.Messaging.Publishers;

public class RabbitMqPublisher : IMessagePublisher
{
    private readonly RabbitMqSettings _settings;

    public RabbitMqPublisher(IOptions<RabbitMqSettings> options)
    {
        _settings = options.Value;
    }

    public Task PublishAsync<T>(
        string queueName,
        T message,
        CancellationToken cancellationToken = default)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        channel.BasicPublish(
            exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body: body);

        return Task.CompletedTask;
    }
}