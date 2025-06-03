namespace Shared.RabbitMQ.Producer;

public interface IRabbitMqProducer
{
    Task Publish(string exchange, string routingKey, byte[] messageBody);
}
