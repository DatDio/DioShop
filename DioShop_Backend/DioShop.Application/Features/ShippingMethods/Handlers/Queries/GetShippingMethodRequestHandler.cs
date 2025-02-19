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
    public class GetShippingMethodRequestHandler : IRequestHandler<GetShippingMethodRequest, ShippingMethodDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetShippingMethodRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShippingMethodDto> Handle(GetShippingMethodRequest request, CancellationToken cancellationToken)
        {
            var shippingMethod = await _unitOfWork.ShippingMethodRepository.Get(request.Id);
            return _mapper.Map<ShippingMethodDto>(shippingMethod);
        }
    }
}
