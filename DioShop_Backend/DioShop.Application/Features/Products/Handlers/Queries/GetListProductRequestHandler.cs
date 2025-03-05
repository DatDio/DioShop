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
using DioShop.Domain.Entities;
using MediatR;

namespace DioShop.Application.Features.Products.Handlers.Queries
{
    public class GetListProductRequestHandler : IRequestHandler<GetProductListRequest, ApiResponse<PagedResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetListProductRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PagedResult>> Handle(GetProductListRequest request, CancellationToken cancellationToken)
        {
            var products = _unitOfWork.ProductRepository.GetProductsWithProductItem(request);

            var pagedResult = CommonUtility.ApplyPaging<Product>(request.Page, request.PageSize, products);

            // Cập nhật Items thành danh sách ProductDto
            pagedResult.Items = _mapper.Map<List<ProductDto>>(pagedResult.Items);

            return new ApiResponse<PagedResult>
            {
                Success = true,
                Message = "Products retrieved successfully",
                Data = pagedResult
            };
        }
    }

}
