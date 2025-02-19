using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Commom;
using DioShop.Application.DTOs.Coupon;
using DioShop.Application.DTOs.OrderItem;
using DioShop.Application.DTOs.ShippingMethod;
using DioShop.Application.DTOs.User;

namespace DioShop.Application.DTOs.Order
{
    public class OrderDto : BaseDto
    {
        public string UserId { get; set; }
        public UserDto User { get; set; }
        public CouponDto? Coupon { get; set; }
        public ShippingMethodDto ShippingMethod { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Discount { get; set; }
        public decimal OrderTotal { get; set; }
        public string Status { get; set; }
        public string PaymentIntentId { get; set; }
        //public string StripeSessionId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
