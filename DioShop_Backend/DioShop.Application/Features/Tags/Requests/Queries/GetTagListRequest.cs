using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Tag;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Tags.Requests.Queries
{
    public class GetTagListRequest : IRequest<ApiResponse<List<TagDto>>>
    {
    }

}
