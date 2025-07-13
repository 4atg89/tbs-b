using RabbitMQ.Client.Events;
using Shared.RabbitMQ.Connection;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Shared.RabbitMQ.Consumer;

internal class RabbitMqConsumer : IRabbitMqConsumer
{
    private readonly IRabbitMqConnectionManager _connectionManager;
    private readonly ILogger<RabbitMqConsumer> _logger;

    public RabbitMqConsumer(IRabbitMqConnectionManager connectionManager, ILogger<RabbitMqConsumer> logger)
    {
        _connectionManager = connectionManager;
        _logger = logger;
    }

    public async Task Subscribe(string exchange, string queue, string routingKey, Action<string> onMessageReceived)
    {
        try
        {
            _logger.LogInformation("Creating RabbitMQ channel for exchange: {Exchange}, queue: {Queue}", exchange, queue);
            
            var channel = await _connectionManager.CreateChannelAsync();

            _logger.LogInformation("Declaring exchange: {Exchange}", exchange);
            await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Direct, durable: true);

            _logger.LogInformation("Declaring queue: {Queue}", queue);
            await channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false);

            _logger.LogInformation("Binding queue {Queue} to exchange {Exchange} with routing key {RoutingKey}", queue, exchange, routingKey);
            await channel.QueueBindAsync(queue: queue, exchange: exchange, routingKey: routingKey);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, args) =>
            {
                try
                {
                    var body = args.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    
                    _logger.LogDebug("Received message from queue {Queue}: {Message}", queue, message);
                    onMessageReceived(message);
                    
                    await channel.BasicAckAsync(args.DeliveryTag, false);
                    _logger.LogDebug("Message acknowledged successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message from queue {Queue}: {ErrorMessage}", queue, ex.Message);
                    await channel.BasicNackAsync(args.DeliveryTag, false, true);
                    _logger.LogWarning("Message nacked and will be requeued");
                }
            };

            _logger.LogInformation("Starting to consume from queue: {Queue}", queue);
            await channel.BasicConsumeAsync(queue: queue, autoAck: false, consumer: consumer);
            
            _logger.LogInformation("Successfully subscribed to RabbitMQ queue: {Queue}", queue);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to subscribe to RabbitMQ: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}
