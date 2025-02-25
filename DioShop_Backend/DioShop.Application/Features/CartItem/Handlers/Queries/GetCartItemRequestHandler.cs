using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.CartItem;
using DioShop.Application.Features.CartItem.Requests.Queries;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.CartItem.Handlers.Queries
{
    public class GetCartItemRequestHandler : IRequestHandler<GetCartItemRequest, ApiResponse<CartItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCartItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CartItemDto>> Handle(GetCartItemRequest request, CancellationToken cancellationToken)
        {
            var cartItem = await _unitOfWork.CartItemRepository.Get(request.Id);

            if (cartItem == null)
            {
                return new ApiResponse<CartItemDto>
                {
                    Success = false,
                    Message = "Cart item not found",
                    Data = null
                };
            }

            return new ApiResponse<CartItemDto>
            {
                Success = true,
                Message = "Cart item retrieved successfully",
                Data = _mapper.Map<CartItemDto>(cartItem)
            };
        }
    }

}
