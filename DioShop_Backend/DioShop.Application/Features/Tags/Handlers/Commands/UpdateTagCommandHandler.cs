using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Tag;
using DioShop.Application.DTOs.Tag.Validators;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Tags.Requests.Commands;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Tags.Handlers.Commands
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, ApiResponse<TagDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<TagDto>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTagDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.TagDto);

            if (!validatorResult.IsValid)
            {
                return new ApiResponse<TagDto>
                {
                    Success = false,
                    Message = validatorResult.Errors.Select(e => e.ErrorMessage).ToList().ToString(),
                    Data =null
                };
            }

            var tag = await _unitOfWork.TagRepository.Get(request.TagDto.Id);

            if (tag == null)
            {
                return new ApiResponse<TagDto>
                {
                    Success = false,
                    Message = "Tag not found"
                };
            }

            _mapper.Map(request.TagDto, tag);

            await _unitOfWork.TagRepository.Update(tag);
            await _unitOfWork.Save();

            return new ApiResponse<TagDto>
            {
                Success = true,
                Message = "Tag updated successfully",
                Data = _mapper.Map<TagDto>(tag)
            };
        }
    }

}
