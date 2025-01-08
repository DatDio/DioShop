using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Commom;
using DioShop.Application.DTOs.ProductItem;

namespace DioShop.Application.DTOs.OrderItem
{
    public class OrderItemDto : BaseDto
    {
        public int OrderId { get; set; }
        public ProductItemDto ProductItem { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
