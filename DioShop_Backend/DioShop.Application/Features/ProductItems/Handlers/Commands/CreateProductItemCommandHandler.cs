using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.ProductItem.Validators;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.ProductItems.Requests.Commands;
using DioShop.Domain.Entities;
using MediatR;

namespace DioShop.Application.Features.ProductItems.Handlers.Commands
{
    public class CreateProductItemCommandHandler : IRequestHandler<CreateProductItemCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductItemDtoValidator(_unitOfWork.ProductRepository);
            var validatorResult = await validator.ValidateAsync(request.ProductItemDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }
            var productItem = _mapper.Map<ProductItem>(request.ProductItemDto);
            productItem = await _unitOfWork.ProductItemRepository.Add(productItem);
            await _unitOfWork.Save();

            return productItem.Id;
        }
    }
}
