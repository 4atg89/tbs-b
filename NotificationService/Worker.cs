using Shared.RabbitMQ.Consumer;

namespace NotificationService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IRabbitMqConsumer _rabbitMqConsumer;
    public Worker(ILogger<Worker> logger, IRabbitMqConsumer rabbitMqConsumer)
    {
        _logger = logger;
        _rabbitMqConsumer = rabbitMqConsumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _rabbitMqConsumer.Subscribe("notifications-exchange", "email-queue", "send.email", (m) => Console.WriteLine($"consumed -> {m}"));
    }
}
