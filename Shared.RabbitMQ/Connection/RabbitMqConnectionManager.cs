using RabbitMQ.Client;
using Shared.RabbitMQ.Extensions;
using Microsoft.Extensions.Logging;

namespace Shared.RabbitMQ.Connection;

internal class RabbitMqConnectionManager : IRabbitMqConnectionManager, IDisposable, IAsyncDisposable
{
    private IConnection? _connection;
    private readonly ConnectionFactory _factory;
    private readonly ILogger<RabbitMqConnectionManager> _logger;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public RabbitMqConnectionManager(RabbitMqOptions options, ILogger<RabbitMqConnectionManager> logger)
    {
        _logger = logger;
        _factory = new ConnectionFactory
        {
            HostName = options.HostName,
            UserName = options.UserName,
            Password = options.Password,
            VirtualHost = options.VirtualHost,
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            RequestedHeartbeat = TimeSpan.FromSeconds(30)
        };
    }

    public async Task<IConnection> GetConnectionAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            if (_connection?.IsOpen != true)
            {
                _logger.LogInformation("RabbitMQ start connection...");
                _connection = await _factory.CreateConnectionAsync();
                _connection.ConnectionShutdownAsync += async (sender, args) =>
                {
                    _logger.LogWarning($"RabbitMQ connection shutdown: {args.Initiator}");
                };
                _logger.LogInformation("RabbitMQ connected successfully");
            }

            return _connection;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<IChannel> CreateChannelAsync()
    {
        var connection = await GetConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        channel.ChannelShutdownAsync += async (sender, args) =>
        {
            _logger.LogWarning("RabbitMQ channel shutdown: {Reason}", args.Initiator);
        };

        return channel;
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
