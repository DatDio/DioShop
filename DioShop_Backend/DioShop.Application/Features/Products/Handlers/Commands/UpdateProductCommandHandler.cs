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
using MediatR;

namespace DioShop.Application.Features.Products.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductDtoValidator(_unitOfWork.BrandRepository, _unitOfWork.CategoryRepository);
            var validatorResult = await validator.ValidateAsync(request.ProductDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var product = await _unitOfWork.ProductRepository.Get(request.ProductDto.Id);

            _mapper.Map(request.ProductDto, product);


            await _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
