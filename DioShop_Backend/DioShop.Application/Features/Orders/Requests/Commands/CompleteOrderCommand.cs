using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DioShop.Application.Features.Orders.Requests.Commands
{
    public class CompleteOrderCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
