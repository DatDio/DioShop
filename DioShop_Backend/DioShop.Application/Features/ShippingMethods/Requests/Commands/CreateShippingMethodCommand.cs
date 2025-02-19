using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.ShippingMethod;
using MediatR;

namespace DioShop.Application.Features.ShippingMethods.Requests.Commands
{
    public class CreateShippingMethodCommand : IRequest<int>
    {
        public CreateShippingMethodDto ShippingMethodDto { get; set; }
    }
}
