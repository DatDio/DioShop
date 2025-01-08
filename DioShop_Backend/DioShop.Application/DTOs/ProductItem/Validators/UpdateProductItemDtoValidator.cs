using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using FluentValidation;

namespace DioShop.Application.DTOs.ProductItem.Validators
{
    public class UpdateProductItemDtoValidator : AbstractValidator<UpdateProductItemDto>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductItemDtoValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(p => p.ProductId)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MustAsync(async (id, token) => {
                var leaveTypeExists = await _productRepository.Exists(id);
                return leaveTypeExists;
            })
                .WithMessage("{PropertyName} does not exist.")
                   ;

            RuleFor(p => p.QuantityInStock)
                   .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(p => p.ImageUrl)
             .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(p => p.Price)
             .NotNull().WithMessage("{PropertyName} is required")
             .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        }
    }
}
