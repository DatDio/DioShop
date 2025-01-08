using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.CartItem;
using DioShop.Application.DTOs.Commom;

namespace DioShop.Application.DTOs.Cart
{
    public class CartDto : BaseDto
    {
        public string UserId { get; set; }
        public ICollection<CartItemDto> CartItems { get; set; }
    }
}
