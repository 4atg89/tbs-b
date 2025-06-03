using RabbitMQ.Client;
using Shared.RabbitMQ.Extensions;

namespace Shared.RabbitMQ.Connection;

internal class RabbitMqConnectionManager : IRabbitMqConnectionManager, IDisposable, IAsyncDisposable
{
    private IConnection? _connection;
    private readonly ConnectionFactory _factory;

    public RabbitMqConnectionManager(RabbitMqOptions options)
    {
        _factory = new ConnectionFactory
        {
            HostName = options.HostName,
            UserName = options.UserName,
            Password = options.Password,
            VirtualHost = options.VirtualHost
        };
    }

    public async Task<IConnection> GetConnectionAsync()
    {
        if (_connection?.IsOpen != true)
        {
            _connection = await _factory.CreateConnectionAsync();
        }
        return _connection;
    }

    public async Task<IChannel> CreateChannelAsync()
    {
        var connection = await GetConnectionAsync();
        return await connection.CreateChannelAsync();
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        _connection?.CloseAsync();
        return _connection?.DisposeAsync() ?? ValueTask.CompletedTask;
    }
}
