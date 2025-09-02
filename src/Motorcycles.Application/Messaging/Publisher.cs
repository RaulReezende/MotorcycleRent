using Motorcycles.Domain.Abstractions.Messaging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Motorcycles.Application.Messaging;

public class Publisher(IRabbitMqConnection rabbitMqConnection) : IMessagePublisher
{
    private readonly IRabbitMqConnection _rabbitMqConnection = rabbitMqConnection;
    public async Task PublishAsync<T>(T message, string topic) where T : class
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        await _rabbitMqConnection.Channel.QueueDeclareAsync(topic, durable: true, exclusive: false, autoDelete: false);

        var basicProperties = new BasicProperties { ContentType = "application/json" };

        await _rabbitMqConnection.Channel.BasicPublishAsync(
            exchange: "",
            routingKey: topic,
            mandatory: false,
            basicProperties: basicProperties,
            body: body
        );
    }
}
