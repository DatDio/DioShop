using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Tag;
using DioShop.Application.Features.Tags.Requests.Queries;
using MediatR;

namespace DioShop.Application.Features.Tags.Handlers.Queries
{
    public class GetTagListRequestHandler : IRequestHandler<GetTagListRequest, List<TagDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTagListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TagDto>> Handle(GetTagListRequest request, CancellationToken cancellationToken)
        {
            var tags = await _unitOfWork.TagRepository.GetAll();
            return _mapper.Map<List<TagDto>>(tags);
        }
    }
}
