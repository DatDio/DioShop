using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.ShippingMethod;
using MediatR;

namespace DioShop.Application.Features.ShippingMethods.Requests.Commands
{
    public class UpdateShippingMethodCommand : IRequest<Unit>
    {
        public UpdateShippingMethodDto ShippingMethodDto { get; set; }
    }
}
