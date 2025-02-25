using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Tags.Requests.Commands
{
    public class DeleteTagCommand : IRequest<ApiResponse<object>>
    {
        public int Id { get; set; }
    }

}
