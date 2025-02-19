using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Coupon;
using DioShop.Application.Features.Coupons.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Features.Coupons.Handlers.Queries
{
	public class GetCouponRequestHandler : IRequestHandler<GetCouponRequest, CouponDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetCouponRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<CouponDto> Handle(GetCouponRequest request, CancellationToken cancellationToken)
		{
			var coupon = await _unitOfWork.CouponRepository.Get(request.Id);
			return _mapper.Map<CouponDto>(coupon);
		}
	}
}
