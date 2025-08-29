using Motorcycles.Domain.Abstractions.Messaging;
using Motorcycles.Infraestructure.RabbitMq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Motorcycles.Application.Messaging;

public class Publisher(RabbitMqConnection rabbitMqConnection) : IMessagePublisher
{
    private readonly RabbitMqConnection _rabbitMqConnection = rabbitMqConnection;
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
