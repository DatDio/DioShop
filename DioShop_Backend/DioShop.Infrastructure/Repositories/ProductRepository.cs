using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Features.Products.Requests.Queries;
using DioShop.Domain.Entities;
using DioShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace DioShop.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public  Product GetProductWithProductItem(int productId)
        {
            return _dbContext.Products.Include(p => p.ProductItems)
                .FirstOrDefault(p => p.Id == productId); ;
        }




        public List<Product> GetProductsWithProductItem(GetProductListRequest request)
        {
            var query = _dbContext.Products
                .Include(p => p.ProductItems) // ✅ Include trước khi ToList()
                .AsNoTracking()
                .ToList(); // ✅ Lấy danh sách sớm để tránh DataReader lỗi

            if (request.CategoryId != null)
            {
                query = query.Where(u => u.CategoryId == request.CategoryId).ToList();
            }
            if (!String.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(u => u.Name.Contains(request.SearchTerm)).ToList();
            }
            if (request.MinPrice.HasValue)
            {
                query = query.Where(u => u.ProductItems.Any(pi => pi.Price >= request.MinPrice)).ToList();
            }
            if (request.MaxPrice.HasValue)
            {
                query = query.Where(u => u.ProductItems.Any(pi => pi.Price <= request.MaxPrice)).ToList();
            }

            return query;
        }


    }
}
