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
    public class GetProductItemRequestHandler : IRequestHandler<GetProductItemRequest, ProductItemDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductItemDto> Handle(GetProductItemRequest request, CancellationToken cancellationToken)
        {
            var productItem = await _unitOfWork.ProductItemRepository.Get(request.Id);



            return _mapper.Map<ProductItemDto>(productItem);
        }
    }
}
