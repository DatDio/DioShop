﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Products.Requests.Commands
{
    public class DeleteProductCommand : IRequest<ApiResponse<object>>
    {
        public int Id { get; set; }
    }

}
