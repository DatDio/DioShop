using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Tag;
using MediatR;

namespace DioShop.Application.Features.Tags.Requests.Queries
{
    public class GetTagRequest : IRequest<TagDto>
    {
        public int Id { get; set; }
    }
}
