using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contants;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Cart;
using DioShop.Application.Features.Cart.Requests.Queries;
using DioShop.Application.Ultils;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DioShop.Application.Features.Cart.Handlers.Queries
{
    public class GetCartRequestHandler : IRequestHandler<GetCartRequest, ApiResponse<CartDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCartRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<CartDto>> Handle(GetCartRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(CustomClaimTypes.Uid)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return new ApiResponse<CartDto>
                {
                    Success = false,
                    Message = "User not authenticated"
                };
            }

            var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);

            if (cart == null)
            {
                return new ApiResponse<CartDto>
                {
                    Success = false,
                    Message = "Cart not found"
                };
            }

            return new ApiResponse<CartDto>
            {
                Success = true,
                Message = "Cart retrieved successfully",
                Data = _mapper.Map<CartDto>(cart)
            };
        }
    }

}
