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
    public class GetCartItemListRequestHandler : IRequestHandler<GetCartItemListRequest, ApiResponse<List<CartItemDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCartItemListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CartItemDto>>> Handle(GetCartItemListRequest request, CancellationToken cancellationToken)
        {
            var cartItems = await _unitOfWork.CartItemRepository.GetAll();

            if (cartItems == null || !cartItems.Any())
            {
                return new ApiResponse<List<CartItemDto>>
                {
                    Success = false,
                    Message = "No cart items found",
                    Data = new List<CartItemDto>()
                };
            }

            return new ApiResponse<List<CartItemDto>>
            {
                Success = true,
                Message = "Cart items retrieved successfully",
                Data = _mapper.Map<List<CartItemDto>>(cartItems)
            };
        }
    }

}
