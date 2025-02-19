using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Coupon;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Coupons.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Features.Coupons.Handlers.Queries
{
	public class GetCouponByCouponCodeRequestHandler : IRequestHandler<GetCouponByCouponCodeRequest, CouponDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetCouponByCouponCodeRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<CouponDto> Handle(GetCouponByCouponCodeRequest request, CancellationToken cancellationToken)
		{
			var coupon = await _unitOfWork.CouponRepository.GetCouponByCode(request.CouponCode);

			if (coupon == null)
			{
				throw new BadRequestException("Coupon is not exist");
			}

			return _mapper.Map<CouponDto>(coupon);
		}
	}
}
