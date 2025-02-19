using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contants;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Order;
using DioShop.Application.Features.Orders.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DioShop.Application.Features.Orders.Handlers.Queries
{
    public class AdminGetOrderListRequestHandler : IRequestHandler<AdminGetOrderListRequest, List<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminGetOrderListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }


        public async Task<List<OrderDto>> Handle(AdminGetOrderListRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid).Value;

            var orders = _unitOfWork.OrderRepository.GetAllOrdersWithDetail();


            return _mapper.Map<List<OrderDto>>(orders);
        }
    }
}
