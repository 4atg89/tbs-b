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
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Starting RabbitMQ");

                await _rabbitMqConsumer.Subscribe("notifications-exchange", "email-queue", "send.email", (message) =>
                    {
                        _logger.LogInformation("Received notification: {Message}", message);
                        //todo redo later on
                        Console.WriteLine($"consumed -> {message}");
                    }
                );

                _logger.LogWarning("RabbitMQ subscription ended");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RabbitMQ subscription");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}