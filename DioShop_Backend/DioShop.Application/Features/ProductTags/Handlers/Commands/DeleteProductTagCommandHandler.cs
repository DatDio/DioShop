using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Features.ProductTags.Requests.Commands;
using MediatR;

namespace DioShop.Application.Features.ProductTags.Handlers.Commands
{
    public class DeleteProductTagCommandHandler : IRequestHandler<DeleteProductTagCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProductTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteProductTagCommand request, CancellationToken cancellationToken)
        {

            var productTag = await _unitOfWork.ProductTagRepository.GetProductTag(request.ProductTagDto.ProductId, request.ProductTagDto.TagId);
            await _unitOfWork.ProductTagRepository.Delete(productTag);
            await _unitOfWork.Save();

            return true;
        }
    }
}
