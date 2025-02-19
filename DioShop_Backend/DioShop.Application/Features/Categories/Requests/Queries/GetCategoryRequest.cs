using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Category;
using MediatR;

namespace DioShop.Application.Features.Categories.Requests.Queries
{
    public class GetCategoryRequest : IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }
}
