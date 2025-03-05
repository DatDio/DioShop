using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Features.ProductItems.Requests.Commands;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.ProductItems.Handlers.Commands
{
    public class DeleteProductItemCommandHandler : IRequestHandler<DeleteProductItemCommand, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProductItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<object>> Handle(DeleteProductItemCommand request, CancellationToken cancellationToken)
        {

            var productItem = await _unitOfWork.ProductItemRepository.Get(request.Id);



            await _unitOfWork.ProductItemRepository.Delete(productItem);
            await _unitOfWork.Save();

            return new ApiResponse<object>
            {
                Success = true,
                Message = "ProductItem deleted successfully",
                Data = null
            };
        }
    }
}
