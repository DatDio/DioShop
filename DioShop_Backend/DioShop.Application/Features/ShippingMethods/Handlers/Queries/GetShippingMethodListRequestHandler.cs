using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.ShippingMethod;
using DioShop.Application.Features.ShippingMethods.Requests.Queries;
using MediatR;

namespace DioShop.Application.Features.ShippingMethods.Handlers.Queries
{
    public class GetShippingMethodListRequestHandler : IRequestHandler<GetShippingMethodListRequest, List<ShippingMethodDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetShippingMethodListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ShippingMethodDto>> Handle(GetShippingMethodListRequest request, CancellationToken cancellationToken)
        {
            var shippingMethods = await _unitOfWork.ShippingMethodRepository.GetAll();
            return _mapper.Map<List<ShippingMethodDto>>(shippingMethods);
        }
    }
}
