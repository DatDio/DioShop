using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Domain.Entities;

namespace DioShop.Application.Contracts.Infrastructure.IRepositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart> GetCartByUserId(string userId);
        Task<decimal> GetTotalMoney(string userId);
        Task<Cart> CreateCart(string userId);
    }
}
