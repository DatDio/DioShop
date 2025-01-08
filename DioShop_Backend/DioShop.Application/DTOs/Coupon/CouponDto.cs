using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Commom;

namespace DioShop.Application.DTOs.Coupon
{
    public class CouponDto : BaseDto
    {
        public string CouponCode { get; set; }
        public decimal MinAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
