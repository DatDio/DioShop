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
namespace DioShop.Application.Features.Brand.Handlers.Commands
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, int>
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
        public async Task<int> Handle(CreateBrandCommand request,
            CancellationToken cancellationToken)
        {
            var validator = new CreateBrandDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.BrandDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }
            var brand = _mapper.Map<DioShop.Domain.Entities.Brand>(request.BrandDto);
            brand = await _unitOfWork.BrandRepository.Add(brand);
            await _unitOfWork.Save();


            //var emailAddress = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

            //var email = new Email
            //{
            //    To = "hieucobappp@gmail.com",
            //    Body = $"Created" +
            //        $"successfully.",
            //    Subject = "Test email"
            //};

            //await _emailSender.SendEmail(email);


            return brand.Id;
        }
    }
}
