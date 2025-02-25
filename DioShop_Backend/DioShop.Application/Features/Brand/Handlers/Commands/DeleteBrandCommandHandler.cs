using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Features.Brand.Requests.Commands;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Brand.Handlers.Commands
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<object>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.BrandRepository.Get(request.Id);
            if (brand == null)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Brand with ID {request.Id} does not exist",
                    Data = null
                };
            }

            await _unitOfWork.BrandRepository.Delete(brand);
            await _unitOfWork.Save();

            return new ApiResponse<object>
            {
                Success = true,
                Message = "Brand deleted successfully",
                Data = null
            };
        }
    }

}
