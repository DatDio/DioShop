using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.DTOs.Coupon
{
    public class CreateCouponDto
    {
        public string CouponCode { get; set; }
        public decimal MinAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
