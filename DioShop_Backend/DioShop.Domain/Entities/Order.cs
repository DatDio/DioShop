using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Domain.Commons;

namespace DioShop.Domain.Entities
{
    public class Order : BaseDomainEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public int ShippingMethodId { get; set; }
        public ShippingMethod ShippingMethod { get; set; }

        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Discount { get; set; }
        public decimal OrderTotal { get; set; }
        public string Status { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
