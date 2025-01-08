using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Commom;

namespace DioShop.Application.DTOs.ProductItem
{
    public class ProductItemDto : BaseDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int QuantityInStock { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
