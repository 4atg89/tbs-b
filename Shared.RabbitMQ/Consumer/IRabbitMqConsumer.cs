namespace Shared.RabbitMQ.Consumer;

public interface IRabbitMqConsumer
{
    Task Subscribe(string exchange, string queue, string routingKey, Action<string> onMessageReceived);
}
