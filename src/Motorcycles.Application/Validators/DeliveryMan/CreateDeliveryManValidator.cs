using FluentValidation;
using Motorcycles.Application.DTOs.Request.DeliveryMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Motorcycles.Application.Validators.DeliveryMan;

public class CreateDeliveryManValidator : AbstractValidator<CreateDeliveryManRequestDto>
{
    public CreateDeliveryManValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Cnpj).NotEmpty().Must(BeValidCnpj);
    }

    private bool BeValidCnpj(string cnpj)
    {
        var regex = new Regex(@"^\d{14}$");
        return regex.IsMatch(cnpj);
    }
}
