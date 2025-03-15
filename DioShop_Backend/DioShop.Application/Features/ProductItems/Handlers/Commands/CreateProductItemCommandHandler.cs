using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Product;
using DioShop.Application.DTOs.ProductItem;
using DioShop.Application.DTOs.ProductItem.Validators;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.ProductItems.Requests.Commands;
using DioShop.Application.Ultils;
using DioShop.Domain.Entities;
using MediatR;

namespace DioShop.Application.Features.ProductItems.Handlers.Commands
{
    public class CreateProductItemCommandHandler : IRequestHandler<CreateProductItemCommand, ApiResponse<ProductItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductItemDto>> Handle(CreateProductItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductItemDtoValidator(_unitOfWork.ProductRepository);
            var validatorResult = await validator.ValidateAsync(request.ProductItemDto);

            if (validatorResult.IsValid == false)
            {
                return new ApiResponse<ProductItemDto>
                {
                    Success = false,
                    Message = validatorResult.Errors.ToString(),
                    Data = null
                };
            }
            ProductItem productItem = null;
            try
            {
                productItem = _mapper.Map<ProductItem>(request.ProductItemDto);
                
                productItem.ImageUrl = await _unitOfWork.FileStoreageRepository.SaveFileAsync(request.ProductItemDto.Image);
                if (String.IsNullOrEmpty(productItem.ImageUrl))
                {
                    return new ApiResponse<ProductItemDto>
                    {
                        Success = false,
                        Message = "ProductItem creation failed: " + "can not upload image",
                        Data = null
                    };
                }
                productItem = await _unitOfWork.ProductItemRepository.Add(productItem);
                await _unitOfWork.Save();

                return new ApiResponse<ProductItemDto>
                {
                    Success = true,
                    Message = "ProductItem created successfully",
                    Data = _mapper.Map<ProductItemDto>(productItem)
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.FileStoreageRepository.DeleteFileAsync(productItem.ImageUrl);
                return new ApiResponse<ProductItemDto>
                {
                    Success = false,
                    Message = "ProductItem creation failed: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
