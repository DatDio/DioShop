using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Features.Products.Requests.Commands;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Products.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<object>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.Get(request.Id);
            if (product == null)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Product not found",
                    Data = null
                };
            }

            await _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.Save();

            return new ApiResponse<object>
            {
                Success = true,
                Message = "Product deleted successfully",
                Data = null
            };
        }
    }

}
