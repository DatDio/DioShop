﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Commom;

namespace DioShop.Application.DTOs.Order
{
    public class UpdateOrderStatusDto : BaseDto
    {
        public string Status { get; set; }
    }
}
