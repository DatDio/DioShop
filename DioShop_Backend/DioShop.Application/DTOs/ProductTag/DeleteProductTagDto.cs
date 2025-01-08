using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.DTOs.ProductTag
{
    public class DeleteProductTagDto
    {
        public int TagId { get; set; }
        public int ProductId { get; set; }
    }
}
