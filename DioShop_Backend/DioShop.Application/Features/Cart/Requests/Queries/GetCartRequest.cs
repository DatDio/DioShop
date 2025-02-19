using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Cart;
using MediatR;

namespace DioShop.Application.Features.Cart.Requests.Queries
{
    public class GetCartRequest : IRequest<CartDto>
    {
    }
}
