using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Cart;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Cart.Requests.Commands
{
    public class CreateCartCommand : IRequest<ApiResponse<CartDto>>
    {
        public string UserId { get; set; }
    }

}
