using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Coupon;
using MediatR;

namespace DioShop.Application.Features.Coupons.Requests.Commands
{
    public class UpdateCouponCommand : IRequest<Unit>
    {
        public UpdateCouponDto CouponDto { get; set; }
    }
}
