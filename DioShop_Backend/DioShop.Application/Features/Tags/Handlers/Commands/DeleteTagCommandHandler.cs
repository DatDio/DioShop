using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Features.Tags.Requests.Commands;
using MediatR;

namespace DioShop.Application.Features.Tags.Handlers.Commands
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {

            var tag = await _unitOfWork.TagRepository.Get(request.Id);



            await _unitOfWork.TagRepository.Delete(tag);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
