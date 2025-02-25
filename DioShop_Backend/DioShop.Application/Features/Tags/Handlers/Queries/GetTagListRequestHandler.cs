using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Tag;
using DioShop.Application.Features.Tags.Requests.Queries;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Tags.Handlers.Queries
{
    public class GetTagListRequestHandler : IRequestHandler<GetTagListRequest, ApiResponse<List<TagDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTagListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<TagDto>>> Handle(GetTagListRequest request, CancellationToken cancellationToken)
        {
            var tags = await _unitOfWork.TagRepository.GetAll();

            if (tags == null || !tags.Any())
            {
                return new ApiResponse<List<TagDto>>
                {
                    Success = false,
                    Message = "No tags found",
                    Data = new List<TagDto>()
                };
            }

            return new ApiResponse<List<TagDto>>
            {
                Success = true,
                Message = "Tags retrieved successfully",
                Data = _mapper.Map<List<TagDto>>(tags)
            };
        }
    }

}
