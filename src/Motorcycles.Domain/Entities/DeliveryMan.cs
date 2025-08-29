using Motorcycles.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motorcycles.Domain.Entities;

public class DeliveryMan
{
    public int Id { get; set; }
    public string Identifier { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string CNPJ { get; set; } = null!;
    public DateTime Birth_Date { get; set; }
    public string CNH_Number { get; set; } = null!;
    public string CNH_Type { get; set; } = null!;

    [NotMapped]
    public CnhType CNH_TypeEnum
    {
        get => (CnhType)Enum.Parse(typeof(CnhType), CNH_Type);
        set => CNH_Type = value.ToString();
    }
    public string? CNH_Image { get; set; }
}
