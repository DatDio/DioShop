using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Brand.Validators;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Brand.Requests.Commands;
using MediatR;

namespace DioShop.Application.Features.Brand.Handlers.Commands
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBrandDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.BrandDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var brand = await _unitOfWork.BrandRepository.Get(request.BrandDto.Id);
            if (brand == null)
            {
                throw new Exception($"Brand with id{request.BrandDto.Id} is not exist" );
            }
            _mapper.Map(request.BrandDto, brand);

          

            await _unitOfWork.BrandRepository.Update(brand);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
