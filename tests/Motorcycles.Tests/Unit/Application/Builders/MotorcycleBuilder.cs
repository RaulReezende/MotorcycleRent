using Motorcycles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Tests.Unit.Application.Builders;

internal class MotorcycleBuilder
{
    private readonly Motorcycle _motorcycle = new()
    {
        Id = 1,
        Identifier = "motoca1",
        Model = "Yamaha-1",
        Plate = "112j310",
        Year = 2022
        
    };
    public static MotorcycleBuilder Create() => new();
    public Motorcycle Build() => _motorcycle;
}
