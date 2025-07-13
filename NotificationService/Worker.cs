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
        _logger.LogInformation("NotificationService Worker started");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Attempting to connect to RabbitMQ...");

                await _rabbitMqConsumer.Subscribe("notifications-exchange", "email-queue", "send.email", (message) =>
                    {
                        _logger.LogInformation("Received notification: {Message}", message);
                        //todo redo later on
                        Console.WriteLine($"consumed -> {message}");
                    }
                );

                _logger.LogWarning("RabbitMQ subscription ended unexpectedly, will retry in 5 seconds");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RabbitMQ subscription: {ErrorMessage}", ex.Message);
                
                // CAIC: Добавляем более детальное логирование для разных типов ошибок
                if (ex.Message.Contains("Connection refused") || ex.Message.Contains("BrokerUnreachableException"))
                {
                    _logger.LogWarning("RabbitMQ connection failed, will retry in 10 seconds");
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }
                else
                {
                    _logger.LogWarning("Other error occurred, will retry in 5 seconds");
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }
        
        _logger.LogInformation("NotificationService Worker stopped");
    }
}