using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Product.Validators;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Products.Requests.Commands;
using DioShop.Domain.Entities;
using MediatR;

namespace DioShop.Application.Features.Products.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductDtoValidator(_unitOfWork.BrandRepository, _unitOfWork.CategoryRepository);
            var validatorResult = await validator.ValidateAsync(request.ProductDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var product = _mapper.Map<Product>(request.ProductDto);
            product = await _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.Save();

            return product.Id;
        }
    }
}
