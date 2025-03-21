﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.Features.Products.Requests.Queries;
using DioShop.Domain.Entities;

namespace DioShop.Application.Contracts.Infrastructure.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Product GetProductWithProductItem(int productId);

       

        List<Product> GetProductsWithProductItem(GetProductListRequest request);

    }
}
