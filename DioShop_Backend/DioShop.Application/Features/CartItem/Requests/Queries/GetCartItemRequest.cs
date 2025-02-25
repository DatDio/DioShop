using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.CartItem;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.CartItem.Requests.Queries
{
    public class GetCartItemRequest : IRequest<ApiResponse<CartItemDto>>
    {
        public int Id { get; set; }
    }

}
