using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Domain.Commons;

namespace DioShop.Domain.Entities
{
    public class Product : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<ProductItem> ProductItems { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }

    }
}
