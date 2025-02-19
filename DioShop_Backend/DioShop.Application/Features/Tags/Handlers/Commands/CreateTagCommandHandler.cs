using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Tag.Validators;
using DioShop.Application.Exceptions;
using DioShop.Application.Features.Tags.Requests.Commands;
using DioShop.Domain.Entities;
using MediatR;

namespace DioShop.Application.Features.Tags.Handlers.Commands
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTagDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.TagDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }
            var tag = _mapper.Map<Tag>(request.TagDto);
            tag = await _unitOfWork.TagRepository.Add(tag);
            await _unitOfWork.Save();

            return tag.Id;
        }
    }
}
