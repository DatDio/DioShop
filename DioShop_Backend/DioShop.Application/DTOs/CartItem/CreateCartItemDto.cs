using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.DTOs.CartItem
{
    public class CreateCartItemDto
    {
        public int CartId { get; set; }
        public int ProductItemId { get; set; }
        public int Quantity { get; set; }
    }
}
