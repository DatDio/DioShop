using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Coupon.Validators;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Coupons.Requests.Commands;
using MediatR;

namespace DioShop.Application.Features.Coupons.Handlers.Commands
{
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCouponDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.CouponDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var coupon = await _unitOfWork.CouponRepository.Get(request.CouponDto.Id);

            _mapper.Map(request.CouponDto, coupon);


            await _unitOfWork.CouponRepository.Update(coupon);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
