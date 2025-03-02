using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Coupon;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Coupons.Requests.Queries
{
    public class GetCouponRequest : IRequest<ApiResponse<CouponDto>>
    {
        public int Id { get; set; }
    }
}
