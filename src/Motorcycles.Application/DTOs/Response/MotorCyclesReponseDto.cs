using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Application.DTOs.Response;

public class MotorCyclesReponseDto
{
    public string Identificador { get; set; } = null!;
    public int Ano { get; set; }
    public string Modelo { get; set; } = null!;
    public string Placa { get; set; } = null!;
}
