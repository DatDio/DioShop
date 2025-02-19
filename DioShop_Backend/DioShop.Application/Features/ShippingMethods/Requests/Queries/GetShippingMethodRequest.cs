using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.ShippingMethod;
using MediatR;

namespace DioShop.Application.Features.ShippingMethods.Requests.Queries
{
    public class GetShippingMethodRequest : IRequest<ShippingMethodDto>
    {
        public int Id { get; set; }
    }
}
