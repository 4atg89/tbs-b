using RabbitMQ.Client;

namespace Shared.RabbitMQ.Connection;

public interface IRabbitMqConnectionManager
{
    Task<IChannel> CreateChannelAsync();
}