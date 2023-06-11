namespace Domain.Messaging;

public interface IMessageBusService
{
    void Publish<T>(T message, string routingKey);
}