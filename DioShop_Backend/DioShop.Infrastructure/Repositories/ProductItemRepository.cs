﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Domain.Entities;
using DioShop.Infrastructure.Data;

namespace DioShop.Infrastructure.Repositories
{
    public class ProductItemRepository : GenericRepository<ProductItem>, IProductItemRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
