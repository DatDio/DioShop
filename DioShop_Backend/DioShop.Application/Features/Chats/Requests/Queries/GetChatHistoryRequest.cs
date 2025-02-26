using DioShop.Application.DTOs.ChatMessage;
using DioShop.Application.Ultils;
using DioShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Features.Chats.Requests.Queries
{
	public class GetChatHistoryRequest : IRequest<ApiResponse<List<MessageDto>>>
	{
		public string SenderId { get; set; }
		public string ReceiverId { get; set; }
	}
}
