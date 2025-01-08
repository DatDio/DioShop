using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Domain.Entities;

namespace DioShop.Application.Contracts.Infrastructure.IRepositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        List<Order> ClientGetAllOrdersWithDetail(string userId);
        List<Order> GetAllOrdersWithDetail();
        Order GetOrderWithDetail(string userId, int orderId);
        Order AdminGetOrderWithDetail(int orderId);

    }
}
