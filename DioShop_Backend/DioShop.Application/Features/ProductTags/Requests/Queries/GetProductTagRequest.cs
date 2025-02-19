using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.ProductTag;
using MediatR;

namespace DioShop.Application.Features.ProductTags.Requests.Queries
{
    public class GetProductTagRequest : IRequest<ProductTagDto>
    {
        public int Id { get; set; }
    }
}
