using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Commom;

namespace DioShop.Application.DTOs.ProductTag
{
    public class ProductTagDto : BaseDto
    {
        public int TagId { get; set; }
        public int ProductId { get; set; }
    }
}
