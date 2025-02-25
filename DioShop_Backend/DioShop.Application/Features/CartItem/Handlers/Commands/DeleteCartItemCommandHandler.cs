using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contants;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.CartItem;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.CartItem.Requests.Commands;
using DioShop.Application.Ultils;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DioShop.Application.Features.CartItem.Handlers.Commands
{
    public class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartItemCommand, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<object>> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Unauthorized user"
                };
            }

            if (!await _unitOfWork.CartItemRepository.IsItemOwnedByUser(request.Id, userId))
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Something went wrong. Item does not belong to user."
                };
            }

            var cartItem = await _unitOfWork.CartItemRepository.Get(request.Id);

            if (cartItem == null)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Cart item not found."
                };
            }

            var cartItemDto = _mapper.Map<CartItemDto>(cartItem);

            await _unitOfWork.CartItemRepository.Delete(cartItem);
            await _unitOfWork.Save();

            return new ApiResponse<object>
            {
                Success = true,
                Message = "Cart item deleted successfully",
                Data = null
            };
        }
    }

}
