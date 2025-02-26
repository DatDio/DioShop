using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Features.Chats.Requests.Queries;
using DioShop.Application.Ultils;
using DioShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Features.Chats.Handlers.Queries
{
	public class GetChatHistoryQueryHandler : IRequestHandler<GetChatHistoryQuery, ApiResponse<List<ChatMessage>>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetChatHistoryQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<ApiResponse<List<ChatMessage>>> Handle(GetChatHistoryQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var messages = await _unitOfWork.ChatMessageRepository.GetMessagesAsync(request.SenderId, request.ReceiverId);
				return new ApiResponse<List<ChatMessage>>
				{
					Success = true,
					Data = messages.ToList()
				};
			}
			catch
			{

			}
			return new ApiResponse<List<ChatMessage>>
			{
				Success = true,
				Message = "Something wents wrong,Cannot Get ChatMessage History!",
				Data = null
			};
		}
	}
}
