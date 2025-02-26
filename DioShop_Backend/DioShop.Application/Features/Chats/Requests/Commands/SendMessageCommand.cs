using DioShop.Application.DTOs.ChatMessage;
using DioShop.Application.Ultils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Features.Chats.Requests.Commands
{
	public class SendMessageCommand : IRequest<ApiResponse<bool>>
	{
        public CreateMessageDto MessageDto { get; set; }
    }
}
