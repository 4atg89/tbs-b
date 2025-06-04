using Shared.RabbitMQ.Connection;
using RabbitMQ.Client;
using System.Text;

namespace Shared.RabbitMQ.Producer;

internal class RabbitMqProducer : IRabbitMqProducer
{
    private readonly IRabbitMqConnectionManager _connectionManager;

    public RabbitMqProducer(IRabbitMqConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task Publish(string exchange, string routingKey, byte[] messageBody)
    {
        var channel = await _connectionManager.CreateChannelAsync();

        //todo move from here
        await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Direct, durable: true);

        var properties = new BasicProperties { Persistent = true };

        await channel.BasicPublishAsync(exchange: exchange,
                             routingKey: routingKey,
                             mandatory: false,
                             basicProperties: properties,
                             body: messageBody);
    }
}
