using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Tag;
using MediatR;

namespace DioShop.Application.Features.Tags.Requests.Commands
{
    public class CreateTagCommand : IRequest<int>
    {
        public CreateTagDto TagDto { get; set; }
    }
}
