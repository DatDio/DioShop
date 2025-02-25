using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Product;
using DioShop.Application.Features.Products.Requests.Queries;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Products.Handlers.Queries
{
    public class GetProductRequestHandler : IRequestHandler<GetProductRequest, ApiResponse<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductDto>> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            var product = _unitOfWork.ProductRepository.GetProductWithProductItem(request.Id);

            if (product == null)
            {
                return new ApiResponse<ProductDto>
                {
                    Success = false,
                    Message = $"Product with ID {request.Id} not found",
                    Data = null
                };
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return new ApiResponse<ProductDto>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = productDto
            };
        }
    }

}
