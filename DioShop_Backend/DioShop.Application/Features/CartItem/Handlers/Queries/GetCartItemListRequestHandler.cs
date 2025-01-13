using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.CartItem;
using DioShop.Application.Features.CartItem.Requests.Queries;
using MediatR;

namespace DioShop.Application.Features.CartItem.Handlers.Queries
{
    public class GetCartItemListRequestHandler : IRequestHandler<GetCartItemListRequest, List<CartItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCartItemListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CartItemDto>> Handle(GetCartItemListRequest request, CancellationToken cancellationToken)
        {
            var cartItems = await _unitOfWork.CartItemRepository.GetAll();
            return _mapper.Map<List<CartItemDto>>(cartItems);
        }
    }
}
