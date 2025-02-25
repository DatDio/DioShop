using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Products.Requests.Queries
{
    public class GetProductListRequest : IRequest<ApiResponse<PagedResult>>
    {
        public int? CategoryId { get; set; }
        public string? SearchTerm { get; set; }
        public string? SortName { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
