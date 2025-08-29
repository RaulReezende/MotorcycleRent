using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Domain.Entities;

public class Rental
{
    public int Id { get; set; }
    public int? DeliveryManId { get; set; }
    public int? MotorcycleId { get; set; }
    public decimal DailyValue { get; set; }
    public decimal? TotalValue { get; set; }
    public DateTime InitDate { get; set; }
    public DateTime PrevEndDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? DevolutionDate { get; set; }
    public int Plan { get; set; }
    public virtual DeliveryMan? DeliveryMan { get; set; } = null!;
    public virtual Motorcycle? Motorcycle { get; set; } = null!;
}
