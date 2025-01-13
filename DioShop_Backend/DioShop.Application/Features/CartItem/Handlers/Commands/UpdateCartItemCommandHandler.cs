using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contants;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.CartItem.Validators;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.CartItem.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DioShop.Application.Features.CartItem.Handlers.Commands
{
    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid).Value;

            if (!(await _unitOfWork.CartItemRepository.IsItemOwnedByUser(request.CartItemDto.Id, userId)))
            {
                throw new BadRequestException("Something went wrong");
            }

            var validator = new UpdateCartItemDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.CartItemDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var cartItem = await _unitOfWork.CartItemRepository.Get(request.CartItemDto.Id);


            if (request.IsMinus == true && cartItem.Quantity > 0)
            {
                cartItem.Quantity--;
            }
            else if (request.IsMinus == false && cartItem.Quantity > 0)
            {
                cartItem.Quantity++;
            }


            await _unitOfWork.CartItemRepository.Update(cartItem);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
