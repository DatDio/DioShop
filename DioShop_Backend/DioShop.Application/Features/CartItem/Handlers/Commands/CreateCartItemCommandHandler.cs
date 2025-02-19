using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.CartItem.Validators;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.CartItem.Requests.Commands;
using MediatR;
using DioShop.Domain.Entities;
namespace DioShop.Application.Features.CartItem.Handlers.Commands
{
    public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCartItemDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.CartItemDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }



            var cartItemExist = await _unitOfWork.CartItemRepository
                .GetCartItem(request.CartItemDto.CartId, request.CartItemDto.ProductItemId);

            if (cartItemExist != null)
            {
                cartItemExist.Quantity = request.CartItemDto.Quantity + cartItemExist.Quantity;
                await _unitOfWork.CartItemRepository.Update(cartItemExist);
                await _unitOfWork.Save();
                return cartItemExist.Id;
            }

            var cartItem = _mapper.Map<DioShop.Domain.Entities.CartItem>(request.CartItemDto);
            cartItem = await _unitOfWork.CartItemRepository.Add(cartItem);
            try
            {
				await _unitOfWork.Save();
			}
            catch
            {

            }
            

            return cartItem.Id;
        }
    }
}
