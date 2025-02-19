using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DioShop.Application.Features.Coupons.Requests.Commands
{
    public class DeleteCouponCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
