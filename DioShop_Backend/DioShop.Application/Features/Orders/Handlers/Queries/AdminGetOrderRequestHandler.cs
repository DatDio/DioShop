using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Order;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Orders.Requests.Queries;
using MediatR;

namespace DioShop.Application.Features.Orders.Handlers.Queries
{
    public class AdminGetOrderRequestHandler : IRequestHandler<AdminGetOrderRequest, OrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AdminGetOrderRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(AdminGetOrderRequest request, CancellationToken cancellationToken)
        {
            var order = _unitOfWork.OrderRepository.AdminGetOrderWithDetail(request.Id);

            if (order == null)
            {
                throw new BadRequestException("Order is not exist");
            }

            return _mapper.Map<OrderDto>(order);
        }
    }
}
