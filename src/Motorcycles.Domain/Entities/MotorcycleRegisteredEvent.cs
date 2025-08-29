using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Domain.Entities;

public class MotorcycleRegisteredEvent
{
    public int Id { get; }
    public int MotorcycleId { get; }
    public string Plate { get; }
    public int Year { get; }
    public string Model { get; }
    public DateTime RegisteredAt { get; }

    public MotorcycleRegisteredEvent(int motorcycleId, string plate, int year, string model)
    {
        MotorcycleId = motorcycleId;
        Plate = plate;
        Year = year;
        Model = model;
        RegisteredAt = DateTime.UtcNow;
    }
}
