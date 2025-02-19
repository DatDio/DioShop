using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contants;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Order;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Orders.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DioShop.Application.Features.Orders.Handlers.Queries
{
    public class ClientGetOrderRequestHandler : IRequestHandler<ClientGetOrderRequest, OrderClientDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClientGetOrderRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<OrderClientDto> Handle(ClientGetOrderRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid).Value;
            var order = _unitOfWork.OrderRepository.GetOrderWithDetail(userId, request.Id);

            if (order == null)
            {
                throw new BadRequestException("Order is not exist");
            }

            return _mapper.Map<OrderClientDto>(order);
        }
    }
}
