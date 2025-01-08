using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Domain.Commons;

namespace DioShop.Domain.Entities
{
    public class ShippingMethod : BaseDomainEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; }
    }
}
