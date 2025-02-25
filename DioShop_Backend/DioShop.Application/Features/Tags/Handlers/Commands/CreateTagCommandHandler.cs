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
using DioShop.Domain.Entities;
using MediatR;

namespace DioShop.Application.Features.Tags.Handlers.Commands
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, ApiResponse<TagDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<TagDto>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTagDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.TagDto);

            if (!validatorResult.IsValid)
            {
                return new ApiResponse<TagDto>
                {
                    Success = false,
                    Message = "Validation failed",
                    Data = null
                };
            }

            var tag = _mapper.Map<Tag>(request.TagDto);
            tag = await _unitOfWork.TagRepository.Add(tag);
            await _unitOfWork.Save();

            return new ApiResponse<TagDto>
            {
                Success = true,
                Message = "Tag created successfully",
                Data = _mapper.Map<TagDto>(tag)
            };
        }
    }

}
