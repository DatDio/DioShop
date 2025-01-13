using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DioShop.Application.Features.CartItem.Requests.Commands
{
    public class DeleteCartItemCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
