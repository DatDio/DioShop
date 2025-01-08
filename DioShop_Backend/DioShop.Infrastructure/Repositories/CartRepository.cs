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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cart> CreateCart(string userId)
        {
            var cart = new Cart()
            {
                UserId = userId,
            };

            await _dbContext.Carts.AddAsync(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            var cart = await _dbContext.Carts.Include(u => u.CartItems).ThenInclude(u => u.ProductItem).FirstOrDefaultAsync(c => c.UserId == userId);

            return cart;
        }

        public async Task<decimal> GetTotalMoney(string userId)
        {
            var cart = await _dbContext.Carts.Include(u => u.CartItems)
                .ThenInclude(u => u.ProductItem).FirstOrDefaultAsync(c => c.UserId == userId);


            decimal total = 0;

            foreach (var cartItem in cart.CartItems)
            {
                total += cartItem.ProductItem.Price * cartItem.Quantity;
            }

            return total;
        }
    }
}
