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
    public class GetBrandRequestHandler : IRequestHandler<GetBrandRequest, BrandDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBrandRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BrandDto> Handle(GetBrandRequest request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.BrandRepository.Get(request.Id);
            return _mapper.Map<BrandDto>(brand);
        }
    }
}
