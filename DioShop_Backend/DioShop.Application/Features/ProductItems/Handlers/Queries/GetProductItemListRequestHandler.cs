using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.ProductItem;
using DioShop.Application.Features.ProductItems.Requests.Queries;
using MediatR;

namespace DioShop.Application.Features.ProductItems.Handlers.Queries
{
    public class GetProductItemListRequestHandler : IRequestHandler<GetProductItemListRequest, List<ProductItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductItemListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProductItemDto>> Handle(GetProductItemListRequest request, CancellationToken cancellationToken)
        {
            var productItems = await _unitOfWork.ProductItemRepository.GetAll();

            return _mapper.Map<List<ProductItemDto>>(productItems);
        }
    }
}
