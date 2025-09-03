using Motorcycles.Application.DTOs.Request.DeliveryMan;
using Motorcycles.Domain.Entities;
using Motorcycles.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Tests.Unit.Application.Builders;

public class DeliveryManBuilder
{
    private readonly DeliveryMan _deliveryMan = new()
    {
        Id = 1,
        Identifier = "motoca1",
        Name = "Roberval",
        CNPJ = "129078361",
        Birth_Date = new DateTime(1970, 09, 10),
        CNH_Number = "12908371",
        CNH_Type = "A",
    };
    public static DeliveryManBuilder Create() => new();
    public DeliveryMan Build() => _deliveryMan;
}
