using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Brand;
using MediatR;

namespace DioShop.Application.Features.Brand.Requests.Queries
{
    public class GetBrandRequest : IRequest<BrandDto>
    {
        public int Id { get; set; }
    }
}
