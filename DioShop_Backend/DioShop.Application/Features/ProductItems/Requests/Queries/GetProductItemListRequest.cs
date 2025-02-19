﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.ProductItem;
using MediatR;

namespace DioShop.Application.Features.ProductItems.Requests.Queries
{
    public class GetProductItemListRequest : IRequest<List<ProductItemDto>>
    {
    }
}
