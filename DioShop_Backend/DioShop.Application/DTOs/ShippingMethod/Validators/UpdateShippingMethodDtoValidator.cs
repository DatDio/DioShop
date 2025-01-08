using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DioShop.Application.DTOs.ShippingMethod.Validators
{
    public class UpdateShippingMethodDtoValidator : AbstractValidator<UpdateShippingMethodDto>
    {
        public UpdateShippingMethodDtoValidator()
        {
            RuleFor(p => p.Name)
                  .NotEmpty().WithMessage("{PropertyName} is required")
                  .NotNull()
                  .MaximumLength(10).WithMessage("{PropertyName} must not exceed 10 characters.");

            RuleFor(p => p.Price)
               .GreaterThan(0).WithMessage("{PropertyName} must be at least 1");
        }
    }
}
