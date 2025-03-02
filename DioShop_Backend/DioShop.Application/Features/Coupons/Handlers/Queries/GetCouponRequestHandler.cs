using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Coupon;
using DioShop.Application.Features.Coupons.Requests.Queries;
using DioShop.Application.Ultils;
using DioShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Features.Coupons.Handlers.Queries
{
    public class GetCouponRequestHandler : IRequestHandler<GetCouponRequest, ApiResponse<CouponDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCouponRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CouponDto>> Handle(GetCouponRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var coupon = await _unitOfWork.CouponRepository.Get(request.Id);
                return new ApiResponse<CouponDto>
                {
                    Success = true,
                    Message = "Coupon found",
                    Data = _mapper.Map<CouponDto>(coupon)
                };

            }
            catch
            {

            }
            return new ApiResponse<CouponDto>
            {
                Success = false,
                Message = "Coupon not found",
                Data = null
            };
        }
    }
}
