using FluentValidation;
using Motorcycles.Application.DTOs.Request;
using Motorcycles.Application.DTOs.Request.Motorcycles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Application.Validators.Motorcycle;

public class CreateMotorcycleValidator : AbstractValidator<CreateMotorcycleRequestDto>
{
    public CreateMotorcycleValidator()
    {
        RuleFor(x => x.Identifier).NotEmpty();
        RuleFor(x => x.Year).NotEmpty().GreaterThan(1950);
        RuleFor(x => x.Model).NotEmpty();
        RuleFor(x => x.PlateNumber).NotEmpty();
    }
}
