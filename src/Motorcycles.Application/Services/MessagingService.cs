using Motorcycles.Application.DTOs.Request.Motorcycles;
using Motorcycles.Domain.Abstractions.Messaging;
using Motorcycles.Domain.Abstractions.Repositories;
using Motorcycles.Domain.Entities;

namespace Motorcycles.Application.Services;

public class MessagingService(IMessagePublisher messagePublisher) : IMessageService
{
    private readonly IMessagePublisher _messagePublisher = messagePublisher;
    public async Task PublishMotorcycleRegisteredEventAsync(Motorcycle motorcycle)
    {
        var @event = new MotorcycleRegisteredEvent(
            motorcycle.Id,
            motorcycle.Plate,
            motorcycle.Year,
            motorcycle.Model);

        await _messagePublisher.PublishAsync(
            @event,
            "motorcycle.registered");
    }
}
