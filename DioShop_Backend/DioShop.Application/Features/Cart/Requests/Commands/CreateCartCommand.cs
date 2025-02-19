using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Cart;
using MediatR;

namespace DioShop.Application.Features.Cart.Requests.Commands
{
    public class CreateCartCommand : IRequest<CartDto>
    {
        public string UserId { get; set; }
    }
}
