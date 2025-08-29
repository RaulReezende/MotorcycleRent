using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Domain.Entities;

public class Motorcycle
{
    public int Id { get; set; }
    public string Identifier { get; set; } = null!;
    public int Year { get; set; }
    public string Model { get; set; } = null!;
    public string Plate { get; set; } = null!;
}
