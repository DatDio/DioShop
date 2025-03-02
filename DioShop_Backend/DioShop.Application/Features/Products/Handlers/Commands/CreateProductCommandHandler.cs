using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Product;
using DioShop.Application.DTOs.Product.Validators;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Products.Requests.Commands;
using DioShop.Application.Ultils;
using DioShop.Domain.Entities;
using MediatR;

namespace DioShop.Application.Features.Products.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductDtoValidator(_unitOfWork.BrandRepository, _unitOfWork.CategoryRepository);
            var validatorResult = await validator.ValidateAsync(request.ProductDto);

            if (!validatorResult.IsValid)
            {
                return new ApiResponse<ProductDto>
                {
                    Success = false,
                    Message = "Validation failed",
                    Data = null
                };
            }
            request.ProductDto.ImageUrl = await _unitOfWork.FileStoreageRepository.SaveFileAsync(request.ProductDto.Image);
            var product = _mapper.Map<Product>(request.ProductDto);
            try
            {
                product = await _unitOfWork.ProductRepository.Add(product);
                await _unitOfWork.Save();

                var createdProductDto = _mapper.Map<ProductDto>(product);

                return new ApiResponse<ProductDto>
                {
                    Success = true,
                    Message = "Product created successfully",
                    Data = createdProductDto
                };
            }
            catch
            {
                await _unitOfWork.FileStoreageRepository.DeleteFileAsync(request.ProductDto.ImageUrl);

            }
            return new ApiResponse<ProductDto>
            {
                Success = false,
                Message = " failed",
                Data = null
            };
        }
    }

}
