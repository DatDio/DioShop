using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Coupon;
using MediatR;

namespace DioShop.Application.Features.Coupons.Requests.Commands
{
    public class CreateCouponCommand : IRequest<int>
    {
        public CreateCouponDto CouponDto { get; set; }
    }
}
