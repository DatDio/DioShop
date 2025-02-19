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
using DioShop.Domain.Entities;
using MediatR;

namespace DioShop.Application.Features.ShippingMethods.Handlers.Commands
{
    public class CreateShippingMethodCommandHandler : IRequestHandler<CreateShippingMethodCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateShippingMethodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateShippingMethodCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateShippingMethodDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.ShippingMethodDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }
            var shippingMethod = _mapper.Map<ShippingMethod>(request.ShippingMethodDto);
            shippingMethod = await _unitOfWork.ShippingMethodRepository.Add(shippingMethod);
            await _unitOfWork.Save();

            return shippingMethod.Id;
        }
    }
}
