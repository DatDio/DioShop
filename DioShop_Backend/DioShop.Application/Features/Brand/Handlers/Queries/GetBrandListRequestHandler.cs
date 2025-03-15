using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Brand;
using DioShop.Application.Features.Brand.Requests.Queries;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Brand.Handlers.Queries
{
    public class GetBrandListRequestHandler : IRequestHandler<GetBrandListRequest, ApiResponse<List<BrandDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBrandListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<BrandDto>>> Handle(GetBrandListRequest request, CancellationToken cancellationToken)
        {
            var brands = await _unitOfWork.BrandRepository.GetAll();
            var branDto = _mapper.Map<List<BrandDto>>(brands);
            if (branDto.Count == 0)
            {
                return new ApiResponse<List<BrandDto>>
                {
                    Success = false,
                    Message = "",
                    Data = null
                };
            }
            return new ApiResponse<List<BrandDto>>
            {
                Success = true,
                Message = null,
                Data = branDto
            }; ;
        }
    }
}
