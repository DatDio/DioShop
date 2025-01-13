using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.CartItem;
using MediatR;

namespace DioShop.Application.Features.CartItem.Requests.Commands
{
    public class UpdateCartItemCommand : IRequest<Unit>
    {
        public UpdateCartItemDto CartItemDto { get; set; }
        public bool IsMinus { get; set; }
    }
}
