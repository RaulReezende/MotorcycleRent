using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Motorcycles.Application.Abstractions.Services;
using Motorcycles.Domain.Entities;
using Motorcycles.Infraestructure.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace Motorcycle.Consumer;

public class MotorcycleConsumerWorker(
    IServiceScopeFactory serviceFactory, 
    RabbitMqConnection rabbitMqConnection,
    ILogger<MotorcycleConsumerWorker> logger) : BackgroundService
{
    private readonly ILogger<MotorcycleConsumerWorker> _logger = logger;
    private readonly IServiceScopeFactory _serviceFactory = serviceFactory;
    private readonly RabbitMqConnection _rabbitMqConnection = rabbitMqConnection;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Motorcycle Consumer Worker started");

        try
        {
            await _rabbitMqConnection.Channel.ExchangeDeclareAsync(exchange: "motorcycle.registered", type: ExchangeType.Fanout);
            await _rabbitMqConnection.Channel.QueueDeclareAsync("motorcycle.registered", durable: true, exclusive: false, autoDelete: false);
            await _rabbitMqConnection.Channel.QueueBindAsync(queue: "motorcycle.registered", exchange: "motorcycle.registered", routingKey: string.Empty);
            var consumer = new AsyncEventingBasicConsumer(_rabbitMqConnection.Channel);

            using var scope = _serviceFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IMotorcycleEventService>();

            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = JsonSerializer.Deserialize<MotorcycleRegisteredEvent>(Encoding.UTF8.GetString(body));
                if (message != null)
                {
                    await handler.CreateMotorcycleEvent(message);   
                }
            };
            await _rabbitMqConnection.Channel.BasicConsumeAsync("motorcycle.registered", autoAck: false, consumer: consumer);
            Console.ReadLine();
        }
            catch (Exception)
            {
                throw;
            }
        }
}