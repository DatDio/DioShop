using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contants;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Order.Validator;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Orders.Requests.Commands;
using MediatR;

namespace DioShop.Application.Features.Orders.Handlers.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateOrderStatusDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.OrderDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var order = await _unitOfWork.OrderRepository.Get(request.OrderDto.Id);
            if (order != null)
            {
                //if (request.OrderDto.Status == OrderStatus.Status_Cancelled)
                //{
                //    //we will give refund
                //    var options = new RefundCreateOptions
                //    {
                //        Reason = RefundReasons.RequestedByCustomer,
                //        PaymentIntent = order.PaymentIntentId
                //    };

                //    var service = new RefundService();
                //    Refund refund = service.Create(options);
                //}
                order.Status = request.OrderDto.Status;
                await _unitOfWork.Save();
            }
            else
            {
                throw new BadRequestException("Order is not exist");
            }

            return Unit.Value;
        }
    }
}
