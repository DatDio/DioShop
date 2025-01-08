using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Domain.Entities;

namespace DioShop.Application.Contracts.Infrastructure.IRepositories
{
    public interface IProductTagRepository : IGenericRepository<ProductTag>
    {
        public Task<ProductTag> GetProductTag(int productId, int tagId);

    }
}
