using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Order;
using MediatR;

namespace DioShop.Application.Features.Orders.Requests.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        public CreateOrderDto OrderDto { get; set; }
    }
}
