using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Domain.Entities;
using DioShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DioShop.Infrastructure.Repositories
{
    public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CouponRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Coupon?> GetCouponByCode(string code)
        {
            var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(u => u.CouponCode == code);


            return coupon;
        }
    }
}
