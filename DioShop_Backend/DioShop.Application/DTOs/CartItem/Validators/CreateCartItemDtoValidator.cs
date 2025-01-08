using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DioShop.Application.DTOs.CartItem.Validators
{
    public  class CreateCartItemDtoValidator:AbstractValidator<CreateCartItemDto>
    {
        public CreateCartItemDtoValidator()
        {
            RuleFor(x => x.CartId).NotEmpty();
            RuleFor(x => x.ProductItemId).NotEmpty();
            RuleFor(p => p.ProductItemId)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull();

            RuleFor(p => p.Quantity)
              .NotEmpty().WithMessage("{PropertyName} is required")
              .GreaterThan(0).WithMessage("{PropertyName} must be at least 1");
        }
    }
}
