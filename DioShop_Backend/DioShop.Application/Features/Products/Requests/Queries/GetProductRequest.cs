using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Product;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Products.Requests.Queries
{
    public class GetProductRequest : IRequest<ApiResponse<ProductDto>>
    {
        public int Id { get; set; }
    }

}
