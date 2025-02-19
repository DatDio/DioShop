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
    public class GetTagRequestHandler : IRequestHandler<GetTagRequest, TagDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTagRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TagDto> Handle(GetTagRequest request, CancellationToken cancellationToken)
        {
            var tag = await _unitOfWork.TagRepository.Get(request.Id);
            return _mapper.Map<TagDto>(tag);
        }
    }
}
