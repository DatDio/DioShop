using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Domain.Commons;

namespace DioShop.Domain.Entities
{
    public class OrderItem : BaseDomainEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }
        public int Quantity { get; set; }
    }
}
