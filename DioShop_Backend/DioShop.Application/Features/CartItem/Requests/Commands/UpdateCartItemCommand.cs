using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.CartItem;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.CartItem.Requests.Commands
{
    public class UpdateCartItemCommand : IRequest<ApiResponse<CartItemDto>>
    {
        public UpdateCartItemDto CartItemDto { get; set; }
        public bool IsMinus { get; set; }
    }

}
