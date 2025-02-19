using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Cart;
using DioShop.Application.Features.Cart.Requests.Commands;
using MediatR;

namespace DioShop.Application.Features.Cart.Handlers.Commands
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, CartDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CartDto> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {

            var cart = await _unitOfWork.CartRepository.CreateCart(request.UserId);

            await _unitOfWork.Save();

            return _mapper.Map<CartDto>(cart);
        }
    }
}
