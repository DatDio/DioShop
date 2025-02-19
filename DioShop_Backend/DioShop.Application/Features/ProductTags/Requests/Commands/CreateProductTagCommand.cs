using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.ProductTag;
using MediatR;

namespace DioShop.Application.Features.ProductTags.Requests.Commands
{
    public class CreateProductTagCommand : IRequest<CreateProductTagDto>
    {
        public CreateProductTagDto ProductTagDto { get; set; }
    }
}
