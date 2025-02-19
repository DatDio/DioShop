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
	public class GetCouponListRequestHandler : IRequestHandler<GetCouponListRequest, List<CouponDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetCouponListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<List<CouponDto>> Handle(GetCouponListRequest request, CancellationToken cancellationToken)
		{
			var coupons = await _unitOfWork.CouponRepository.GetAll();
			return _mapper.Map<List<CouponDto>>(coupons);
		}
	}
}
