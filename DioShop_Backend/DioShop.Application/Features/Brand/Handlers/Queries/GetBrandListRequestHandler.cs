using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Brand;
using DioShop.Application.Features.Brand.Requests.Queries;
using MediatR;

namespace DioShop.Application.Features.Brand.Handlers.Queries
{
    public class GetBrandListRequestHandler : IRequestHandler<GetBrandListRequest, List<BrandDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBrandListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BrandDto>> Handle(GetBrandListRequest request, CancellationToken cancellationToken)
        {
            var brands = await _unitOfWork.BrandRepository.GetAll();
            return _mapper.Map<List<BrandDto>>(brands);
        }
    }
}
