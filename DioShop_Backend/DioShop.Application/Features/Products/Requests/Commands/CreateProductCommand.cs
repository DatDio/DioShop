using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Product;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Products.Requests.Commands
{
    public class CreateProductCommand : IRequest<ApiResponse<ProductDto>>
    {
        public CreateProductDto ProductDto { get; set; }
    }

}
