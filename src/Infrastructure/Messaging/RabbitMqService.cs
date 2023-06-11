using System.Text;
using Domain.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infrastructure.Messaging;

public sealed class RabbitMqService : IMessageBusService
{
    private readonly IConnection _connection;

    public RabbitMqService()
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        _connection = connectionFactory.CreateConnection();
    }

    public void Publish<T>(T message, string routingKey)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(
            queue: routingKey,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        string? stringMessage = message!.ToString();

        var byteArray = Encoding.UTF8.GetBytes(stringMessage);

        channel.BasicPublish(
        exchange: "",
        routingKey: routingKey,
        basicProperties: null,
        body: byteArray
        );
    }
}