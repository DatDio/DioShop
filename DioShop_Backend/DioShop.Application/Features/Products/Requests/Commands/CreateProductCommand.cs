using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Product;
using MediatR;

namespace DioShop.Application.Features.Products.Requests.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public CreateProductDto ProductDto { get; set; }
    }
}
