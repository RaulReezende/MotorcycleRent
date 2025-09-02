using Motorcycles.Application.DTOs.Request.DeliveryMan;
using Motorcycles.Domain.Enums;

namespace Motorcycles.Tests.Unit.Application.Builders;

public class CreateDeliveryManDtoBuilder
{
    private CreateDeliveryManRequestDto _deliveryMan = new()
    {
        Identifier = "motoca1",
        Name = "Roberval",
        Cnpj = "129078361",
        DateOfBirth = new DateTime(1970, 09, 10),
        CnhNumber = "12908371",
        CnhType = CnhType.A,
    };

    public static CreateDeliveryManDtoBuilder Create() => new CreateDeliveryManDtoBuilder();
    public CreateDeliveryManRequestDto Build() => _deliveryMan;
}