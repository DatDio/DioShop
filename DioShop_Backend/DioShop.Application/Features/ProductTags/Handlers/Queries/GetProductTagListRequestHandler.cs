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
    public class GetProductTagListRequestHandler : IRequestHandler<GetProductTagListRequest, List<ProductTagDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductTagListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProductTagDto>> Handle(GetProductTagListRequest request, CancellationToken cancellationToken)
        {
            var holds = await _unitOfWork.ProductTagRepository.GetAll();
            return _mapper.Map<List<ProductTagDto>>(holds);
        }
    }
}
