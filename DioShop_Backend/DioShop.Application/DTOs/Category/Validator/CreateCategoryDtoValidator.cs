using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DioShop.Application.DTOs.Category.Validator
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(p => p.Name)
             .NotEmpty().WithMessage("{PropertyName} is required")
             .NotNull()
             .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");

            //RuleFor(p => p.Description)
            // .NotEmpty().WithMessage("{PropertyName} is required")
            // .NotNull()
            // .MaximumLength(300).WithMessage("{PropertyName} must not exceed 300 characters.");

            //RuleFor(p => p.ImageUrl)
            //.NotEmpty().WithMessage("{PropertyName} is required")
            //.NotNull();
        }
    }
}
