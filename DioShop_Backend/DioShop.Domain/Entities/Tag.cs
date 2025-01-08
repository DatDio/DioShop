using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Domain.Commons;

namespace DioShop.Domain.Entities
{
    public class Tag : BaseDomainEntity
    {
        public string Name { get; set; }
        public List<ProductTag> ProductTags { get; }

    }
}
