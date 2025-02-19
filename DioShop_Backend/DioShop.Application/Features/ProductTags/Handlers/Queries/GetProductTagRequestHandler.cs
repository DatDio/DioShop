using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.ProductTag;
using DioShop.Application.Features.ProductTags.Requests.Queries;
using MediatR;

namespace DioShop.Application.Features.ProductTags.Handlers.Queries
{
    public class GetProductTagRequestHandler : IRequestHandler<GetProductTagRequest, ProductTagDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductTagRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductTagDto> Handle(GetProductTagRequest request, CancellationToken cancellationToken)
        {
            var hold = await _unitOfWork.ProductTagRepository.Get(request.Id);
            return _mapper.Map<ProductTagDto>(hold);
        }
    }
}
