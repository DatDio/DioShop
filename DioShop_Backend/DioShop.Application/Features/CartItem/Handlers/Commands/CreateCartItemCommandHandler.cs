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
using DioShop.Application.DTOs.CartItem;
namespace DioShop.Application.Features.CartItem.Handlers.Commands
{
    public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, CartItemDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CartItemDto> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
        {
            // Validate dữ liệu đầu vào
            var validator = new CreateCartItemDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.CartItemDto);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            // Kiểm tra xem CartItem đã tồn tại chưa
            var cartItemExist = await _unitOfWork.CartItemRepository
                .GetCartItem(request.CartItemDto.CartId, request.CartItemDto.ProductItemId);

            if (cartItemExist != null)
            {
                cartItemExist.Quantity += request.CartItemDto.Quantity;
                await _unitOfWork.CartItemRepository.Update(cartItemExist);
                await _unitOfWork.Save();
                return _mapper.Map<CartItemDto>(cartItemExist);
            }

            // Tạo mới CartItem
            var cartItem = _mapper.Map<DioShop.Domain.Entities.CartItem>(request.CartItemDto);
            cartItem = await _unitOfWork.CartItemRepository.Add(cartItem);

            await _unitOfWork.Save();

            return _mapper.Map<CartItemDto>(cartItem);
        }
    }

}
