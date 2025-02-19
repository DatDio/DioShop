using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Order;
using MediatR;

namespace DioShop.Application.Features.Orders.Requests.Queries
{
    public class AdminGetOrderRequest : IRequest<OrderDto>
    {
        public int Id { get; set; }
    }
}
