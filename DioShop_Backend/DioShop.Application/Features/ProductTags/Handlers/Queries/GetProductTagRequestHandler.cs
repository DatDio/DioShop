using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.ProductTag;
using DioShop.Application.Features.ProductTags.Requests.Queries;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.ProductTags.Handlers.Queries
{
    public class GetProductTagRequestHandler : IRequestHandler<GetProductTagRequest, ApiResponse<ProductTagDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductTagRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductTagDto>> Handle(GetProductTagRequest request, CancellationToken cancellationToken)
        {
            var hold = await _unitOfWork.ProductTagRepository.Get(request.Id);
            if (hold == null)
            {
                return new ApiResponse<ProductTagDto>
                {
                    Success = false,
                    Message = "Product tag not found",
                    Data = null
                };
            }

            return new ApiResponse<ProductTagDto>
            {
                Success = true,
                Data = _mapper.Map<ProductTagDto>(hold)
            };
        }
    }
}
