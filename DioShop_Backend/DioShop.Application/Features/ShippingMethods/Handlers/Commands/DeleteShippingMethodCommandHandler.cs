using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Features.ShippingMethods.Requests.Commands;
using MediatR;

namespace DioShop.Application.Features.ShippingMethods.Handlers.Commands
{
    public class DeleteShippingMethodCommandHandler : IRequestHandler<DeleteShippingMethodCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteShippingMethodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteShippingMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shippingMethod = await _unitOfWork.ShippingMethodRepository.Get(request.Id);



                await _unitOfWork.ShippingMethodRepository.Delete(shippingMethod);
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
