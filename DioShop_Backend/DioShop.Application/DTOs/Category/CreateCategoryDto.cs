using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DioShop.Application.DTOs.Category
{
    public class CreateCategoryDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
