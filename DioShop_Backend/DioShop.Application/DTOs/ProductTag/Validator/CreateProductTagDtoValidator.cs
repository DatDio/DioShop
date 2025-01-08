using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using FluentValidation;

namespace DioShop.Application.DTOs.ProductTag.Validator
{
    public class CreateProductTagDtoValidator : AbstractValidator<CreateProductTagDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductTagDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.ProductId)
            .MustAsync(async (id, token) => {
                var leaveTypeExists = await _unitOfWork.ProductRepository.Exists(id);
                return leaveTypeExists;
            }).WithMessage("{PropertyName} does not exist.")
            .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(p => p.TagId)
            .MustAsync(async (id, token) => {
                var leaveTypeExists = await _unitOfWork.TagRepository.Exists(id);
                return leaveTypeExists;
            }).WithMessage("{PropertyName} does not exist.")
            .NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
