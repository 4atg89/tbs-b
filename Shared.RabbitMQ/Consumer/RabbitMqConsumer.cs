using RabbitMQ.Client.Events;
using Shared.RabbitMQ.Connection;
using RabbitMQ.Client;
using System.Text;

namespace Shared.RabbitMQ.Consumer;

internal class RabbitMqConsumer : IRabbitMqConsumer
{
    private readonly IRabbitMqConnectionManager _connectionManager;

    public RabbitMqConsumer(IRabbitMqConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task Subscribe(string exchange, string queue, string routingKey, Action<string> onMessageReceived)
    {
        try
        {
            var channel = await _connectionManager.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Direct, durable: true);

            await channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false);

            await channel.QueueBindAsync(queue: queue, exchange: exchange, routingKey: routingKey);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, args) =>
            {
                try
                {
                    var body = args.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    
                    onMessageReceived(message);
                    
                    await channel.BasicAckAsync(args.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    await channel.BasicNackAsync(args.DeliveryTag, false, true);
                }
            };

            await channel.BasicConsumeAsync(queue: queue, autoAck: false, consumer: consumer);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
