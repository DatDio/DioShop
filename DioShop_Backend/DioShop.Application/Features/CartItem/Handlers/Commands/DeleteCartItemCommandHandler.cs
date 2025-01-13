using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contants;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.CartItem.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DioShop.Application.Features.CartItem.Handlers.Commands
{
    public class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartItemCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeleteCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid).Value;

            if (!await _unitOfWork.CartItemRepository.IsItemOwnedByUser(request.Id, userId))
            {
                throw new BadRequestException("Something went wrong");
            }

            var cartItem = await _unitOfWork.CartItemRepository.Get(request.Id);


            await _unitOfWork.CartItemRepository.Delete(cartItem);
            await _unitOfWork.Save();

            return Unit.Value;
        }


    }
}
