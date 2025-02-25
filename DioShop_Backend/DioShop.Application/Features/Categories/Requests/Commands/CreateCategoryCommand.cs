using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Category;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Categories.Requests.Commands
{
    public class CreateCategoryCommand : IRequest<ApiResponse<CategoryDto>>
    {
        public CreateCategoryDto CategoryDto { get; set; }
    }
}
