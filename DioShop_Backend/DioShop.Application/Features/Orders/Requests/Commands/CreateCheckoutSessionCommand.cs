using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DioShop.Application.Features.Orders.Requests.Commands
{
    public class CreateCheckoutSessionCommand : IRequest<string>
    {
        public int OrderId { get; set; }
        //public StripeSetupDto StripeSetupDto { get; set; }
    }
}
