using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contants;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.CartItem;
using DioShop.Application.DTOs.CartItem.Validators;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.CartItem.Requests.Commands;
using DioShop.Application.Ultils;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DioShop.Application.Features.CartItem.Handlers.Commands
{
    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, ApiResponse<CartItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<CartItemDto>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return new ApiResponse<CartItemDto>
                {
                    Success = false,
                    Message = "Unauthorized user"
                };
            }

            if (!(await _unitOfWork.CartItemRepository.IsItemOwnedByUser(request.CartItemDto.Id, userId)))
            {
                return new ApiResponse<CartItemDto>
                {
                    Success = false,
                    Message = "Something went wrong. Item does not belong to user."
                };
            }

            var validator = new UpdateCartItemDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.CartItemDto);

            if (!validatorResult.IsValid)
            {
                return new ApiResponse<CartItemDto>
                {
                    Success = false,
                    Message = "Validation failed",
                    Data =  null
                };
            }

            var cartItem = await _unitOfWork.CartItemRepository.Get(request.CartItemDto.Id);

            if (request.IsMinus && cartItem.Quantity > 0)
            {
                cartItem.Quantity--;
            }
            else if (!request.IsMinus)
            {
                cartItem.Quantity++;
            }

            await _unitOfWork.CartItemRepository.Update(cartItem);
            await _unitOfWork.Save();

            var cartItemDto = _mapper.Map<CartItemDto>(cartItem);

            return new ApiResponse<CartItemDto>
            {
                Success = true,
                Message = "Cart item updated successfully",
                Data = cartItemDto
            };
        }
    }

}
