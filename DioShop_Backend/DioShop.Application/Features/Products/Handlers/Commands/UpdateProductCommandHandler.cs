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
using MediatR;

namespace DioShop.Application.Features.Products.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductDtoValidator(_unitOfWork.BrandRepository, _unitOfWork.CategoryRepository);
            var validatorResult = await validator.ValidateAsync(request.ProductDto);

            if (!validatorResult.IsValid)
            {
                return new ApiResponse<ProductDto>
                {
                    Success = false,
                    Message = validatorResult.ToString(),
                    Data = null
                };
            }

            var product = await _unitOfWork.ProductRepository.Get(request.ProductDto.Id);
            if (product == null)
            {
                return new ApiResponse<ProductDto>
                {
                    Success = false,
                    Message = "Product not found",
                    Data = null
                };
            }

            _mapper.Map(request.ProductDto, product);
            await _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.Save();

            var updatedProductDto = _mapper.Map<ProductDto>(product);

            return new ApiResponse<ProductDto>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = updatedProductDto
            };
        }
    }

}
