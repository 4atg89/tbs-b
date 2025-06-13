using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.RabbitMQ.Connection;
using Shared.RabbitMQ.Consumer;
using Shared.RabbitMQ.Producer;

namespace Shared.RabbitMQ.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new RabbitMqOptions();
        var section = configuration.GetSection("RabbitMQ");
        section.Bind(options);
        services.Configure<RabbitMqOptions>(section);

        services.AddSingleton<IRabbitMqConnectionManager>(sp => 
            new RabbitMqConnectionManager(options, sp.GetRequiredService<ILogger<RabbitMqConnectionManager>>()));

        services.AddSingleton<IRabbitMqProducer, RabbitMqProducer>();
        services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();

        return services;
    }
}

public class RabbitMqOptions
{
    public string HostName { get; set; } = "localhost";
    public string UserName { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string VirtualHost { get; set; } = "/";
}
