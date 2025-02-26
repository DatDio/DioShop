using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Features.Chats.Requests.Commands;
using DioShop.Application.Ultils;
using DioShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Features.Chats.Handlers.Commands
{
	public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ApiResponse<bool>>
	{
		private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SendMessageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
            _mapper = mapper;
        }

		public async Task<ApiResponse<bool>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var chatMessage = _mapper.Map<ChatMessage>(request.MessageDto);

                await _unitOfWork.ChatMessageRepository.Add(chatMessage);
				await _unitOfWork.Save();
				return new ApiResponse<bool>
				{
					Success = true,

				};
			}
			catch (Exception ex)
			{

			}

			return new ApiResponse<bool>
			{
				Success = false,

			};

		}
	}
}
