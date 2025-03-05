using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Category;
using DioShop.Application.DTOs.Category.Validator;
using DioShop.Application.DTOs.Product;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Categories.Requests.Commands;
using DioShop.Application.Ultils;
using DioShop.Domain.Entities;
using MediatR;

namespace DioShop.Application.Features.Categories.Handlers.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCategoryDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.CategoryDto);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            Category category = null; // Khai báo category trước để tránh lỗi trong catch

            try
            {
                // Ánh xạ DTO sang Entity
                category = _mapper.Map<Category>(request.CategoryDto);

                // Upload ảnh
                category.ImageUrl = await _unitOfWork.FileStoreageRepository.SaveFileAsync(request.CategoryDto.Image);
                if (string.IsNullOrEmpty(category.ImageUrl))
                {
                    return new ApiResponse<CategoryDto>
                    {
                        Success = false,
                        Message = "Cannot upload image",
                        Data = null
                    };
                }

                // Thêm vào database
                category = await _unitOfWork.CategoryRepository.Add(category);
                await _unitOfWork.Save();

                return new ApiResponse<CategoryDto>
                {
                    Success = true,
                    Message = "Category created successfully",
                    Data = _mapper.Map<CategoryDto>(category)
                };
            }
            catch (Exception ex)
            {
                // Nếu có lỗi và đã upload ảnh, thì xóa ảnh
                if (category != null && !string.IsNullOrEmpty(category.ImageUrl))
                {
                    await _unitOfWork.FileStoreageRepository.DeleteFileAsync(category.ImageUrl);
                }


                return new ApiResponse<CategoryDto>
                {
                    Success = false,
                    Message = "Category creation failed: " + ex.InnerException.Message,
                    Data = null
                };
            }
        }

    }
}
