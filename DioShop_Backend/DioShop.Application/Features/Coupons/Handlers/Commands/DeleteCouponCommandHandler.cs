using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Features.Coupons.Requests.Commands;
using MediatR;

namespace DioShop.Application.Features.Coupons.Handlers.Commands
{
    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var coupon = await _unitOfWork.CouponRepository.Get(request.Id);
                await _unitOfWork.CouponRepository.Delete(coupon);
                await _unitOfWork.Save();
            }
            catch
            {
                return false;
               
            }

            return true;
        }
    }
}
