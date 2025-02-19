using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Category.Validator;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Categories.Requests.Commands;
using DioShop.Domain.Entities;
using MediatR;

namespace DioShop.Application.Features.Categories.Handlers.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCategoryDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.CategoryDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }
            var category = _mapper.Map<Category>(request.CategoryDto);
            category = await _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.Save();

            return category.Id;
        }
    }
}
