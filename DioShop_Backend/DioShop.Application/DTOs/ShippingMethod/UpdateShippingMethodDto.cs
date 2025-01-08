using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Commom;

namespace DioShop.Application.DTOs.ShippingMethod
{
    public class UpdateShippingMethodDto : BaseDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
