using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Contracts.Infrastructure;
using DioShop.Application.Features.Brand.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using DioShop.Application.DTOs.Brand.Validators;
using DioShop.Application.Models;
using System.Security.Claims;
using DioShop.Application.Exceptions;
using DioShop.Domain.Entities;
using DioShop.Application.DTOs.Brand;
using DioShop.Application.Ultils;
namespace DioShop.Application.Features.Brand.Handlers.Commands
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, ApiResponse<BrandDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IEmailSender emailSender,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
        }

        public async Task<ApiResponse<BrandDto>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateBrandDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.BrandDto);

            if (!validatorResult.IsValid)
            {
                return new ApiResponse<BrandDto>
                {
                    Success = false,
                    Message = "Validation failed",
                    Data = null
                };
            }

            var brand = _mapper.Map<DioShop.Domain.Entities.Brand>(request.BrandDto);
            brand = await _unitOfWork.BrandRepository.Add(brand);
            await _unitOfWork.Save();

            var brandDto = _mapper.Map<BrandDto>(brand);

            return new ApiResponse<BrandDto>
            {
                Success = true,
                Message = "Brand created successfully",
                Data = brandDto
            };
        }
    }

}
