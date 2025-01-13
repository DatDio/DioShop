using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DioShop.Application.Features.Brand.Requests.Commands
{
    public class DeleteBrandCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
