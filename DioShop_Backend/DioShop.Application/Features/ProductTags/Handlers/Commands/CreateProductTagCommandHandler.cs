using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.ProductTag;
using DioShop.Application.DTOs.ProductTag.Validator;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.ProductTags.Requests.Commands;
using DioShop.Domain.Entities;
using MediatR;

namespace DioShop.Application.Features.ProductTags.Handlers.Commands
{
    public class CreateProductTagCommandHandler : IRequestHandler<CreateProductTagCommand, CreateProductTagDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateProductTagDto> Handle(CreateProductTagCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductTagDtoValidator(_unitOfWork);
            var validatorResult = await validator.ValidateAsync(request.ProductTagDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var isExist = await _unitOfWork.ProductTagRepository.GetProductTag(request.ProductTagDto.ProductId, request.ProductTagDto.TagId);


            if (isExist != null)
            {
                throw new BadRequestException("It already exists");
            }

            var productTag = _mapper.Map<ProductTag>(request.ProductTagDto);
            productTag = await _unitOfWork.ProductTagRepository.Add(productTag);
            await _unitOfWork.Save();

            return request.ProductTagDto;
        }
    }
}
