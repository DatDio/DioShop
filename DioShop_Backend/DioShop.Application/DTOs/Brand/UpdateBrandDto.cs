﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Commom;

namespace DioShop.Application.DTOs.Brand
{
    public class UpdateBrandDto : BaseDto
    {
        public string Name { get; set; }
    }
}
