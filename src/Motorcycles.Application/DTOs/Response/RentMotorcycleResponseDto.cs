using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Application.DTOs.Response;

public class RentMotorcycleResponseDto
{
    public string Identificador { get; set; }
    public decimal Valor_diaria { get; set; }
    public string Entregador_id { get; set; }
    public string Moto_id { get; set; }
    public DateTime Data_inicio { get; set; }
    public DateTime Data_termino { get; set; }
    public DateTime Data_previsao_termino { get; set; }
    public DateTime? Data_devolucao { get; set; }
}
