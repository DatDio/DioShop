using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.ShippingMethod.Validators;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.ShippingMethods.Requests.Commands;
using MediatR;

namespace DioShop.Application.Features.ShippingMethods.Handlers.Commands
{
    public class UpdateShippingMethodCommandHandler : IRequestHandler<UpdateShippingMethodCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShippingMethodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateShippingMethodCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateShippingMethodDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.ShippingMethodDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var shippingMethod = await _unitOfWork.ShippingMethodRepository.Get(request.ShippingMethodDto.Id);

            _mapper.Map(request.ShippingMethodDto, shippingMethod);


            await _unitOfWork.ShippingMethodRepository.Update(shippingMethod);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
