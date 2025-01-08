using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DioShop.Application.DTOs.Coupon.Validators
{
    public class CreateCouponDtoValidator : AbstractValidator<CreateCouponDto>
    {
        public CreateCouponDtoValidator()
        {
            RuleFor(p => p.CouponCode)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(10).WithMessage("{PropertyName} must not exceed 10 characters.");

            RuleFor(p => p.MinAmount)
               .GreaterThan(0).WithMessage("{PropertyName} must be at least 1");

            RuleFor(p => p.DiscountAmount)
               .GreaterThan(0).WithMessage("{PropertyName} must be at least 1");

        }
    }
}
