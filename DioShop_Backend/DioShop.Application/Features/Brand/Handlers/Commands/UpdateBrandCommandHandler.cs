using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Brand;
using DioShop.Application.DTOs.Brand.Validators;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Brand.Requests.Commands;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Brand.Handlers.Commands
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, ApiResponse<BrandDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<BrandDto>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBrandDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.BrandDto);

            if (!validatorResult.IsValid)
            {
                return new ApiResponse<BrandDto>
                {
                    Success = false,
                    Message = "Validation failed: " + string.Join(", ", validatorResult.Errors.Select(e => e.ErrorMessage)),
                    Data = null
                };
            }

            var brand = await _unitOfWork.BrandRepository.Get(request.BrandDto.Id);
            if (brand == null)
            {
                return new ApiResponse<BrandDto>
                {
                    Success = false,
                    Message = $"Brand with ID {request.BrandDto.Id} does not exist",
                    Data = null
                };
            }

            _mapper.Map(request.BrandDto, brand);
            await _unitOfWork.BrandRepository.Update(brand);
            await _unitOfWork.Save();

            var updatedBrandDto = _mapper.Map<BrandDto>(brand);

            return new ApiResponse<BrandDto>
            {
                Success = true,
                Message = "Brand updated successfully",
                Data = updatedBrandDto
            };
        }
    }


}
