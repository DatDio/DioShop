using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using FluentValidation;

namespace DioShop.Application.DTOs.Product.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateProductDtoValidator(IBrandRepository brandRepository, ICategoryRepository categoryRepository)
        {
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;


            RuleFor(p => p.Name)
             .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(p => p.Description)
             .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(p => p.BrandId)
                 .MustAsync(async (id, token) => {
                     var brandExists = await _brandRepository.Exists(id);
                     return brandExists;
                 })
                .WithMessage("{PropertyName} does not exist.")
                .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(p => p.CategoryId)
                .MustAsync(async (id, token) => {
                    var categoryExists = await _categoryRepository.Exists(id);
                    return categoryExists;
                })
                .WithMessage("{PropertyName} does not exist.")
                .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(p => p.ImageUrl)
             .NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
