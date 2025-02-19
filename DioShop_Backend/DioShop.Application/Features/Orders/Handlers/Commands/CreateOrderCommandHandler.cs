using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contants;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Contracts.Infrastructure;
using DioShop.Application.DTOs.Order.Validator;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Orders.Requests.Commands;
using DioShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DioShop.Application.Features.Orders.Handlers.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IEmailSender emailSender,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateOrderDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.OrderDto);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid).Value;

            var order = new Order
            {
                UserId = userId,
                ShippingMethodId = request.OrderDto.ShippingMethodId,
                CustomerName = request.OrderDto.CustomerName,
                Address = request.OrderDto.Address,
                PhoneNumber = request.OrderDto.PhoneNumber,
                OrderTotal = await _unitOfWork.CartRepository.GetTotalMoney(userId),
                Status = OrderStatus.Status_Pending
            };

            if (request.OrderDto.CouponId != 0)
            {
                var coupon = await _unitOfWork.CouponRepository.Get(request.OrderDto.CouponId);

                if (order.OrderTotal < coupon.MinAmount)
                {
                    throw new BadRequestException("Not support coupon because total price is smaller than min amount of this coupon");
                }
                order.CouponId = request.OrderDto.CouponId;
                order.Discount = coupon.DiscountAmount;
            }
            else
            {
                order.Discount = 0;
            }

            order = await _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.Save();

            var listOrderItems = new List<OrderItem>();

            if (request.OrderDto.CartItemIdList.Count == 0)
            {
                throw new BadRequestException("Please Add to Cart before create order");
            }

            foreach (var id in request.OrderDto.CartItemIdList)
            {
                var cartItemDetail = await _unitOfWork.CartItemRepository.GetCartItemDetail(id);
                listOrderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductItemId = cartItemDetail.ProductItemId,
                    Quantity = cartItemDetail.Quantity
                });
            }

            await _unitOfWork.OrderItemRepository.AddRange(listOrderItems);

            //After create order item
            foreach (var cartItemId in request.OrderDto.CartItemIdList)
            {
                await _unitOfWork.CartItemRepository.DeleteById(cartItemId);
            }

            await _unitOfWork.Save();


            //var emailAddress = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

            //var email = new Email
            //{
            //    To = emailAddress,
            //    Body = $"Created" +
            //        $"successfully.",
            //    Subject = "Test email"
            //};

            //await _emailSender.SendEmail(email);

            return order.Id;
        }
    }
}
