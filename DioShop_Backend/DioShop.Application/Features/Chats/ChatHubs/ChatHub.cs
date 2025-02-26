using DioShop.Application.Features.Chats.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Features.Chats.ChatHubs
{
	public class ChatHub : Hub
	{
		private readonly IMediator _mediator;

		public ChatHub(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task SendMessage(string receiverId, string message)
		{
			var senderId = Context.UserIdentifier; // Lấy ID người gửi từ token

			if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId) || string.IsNullOrEmpty(message))
				return;

			var command = new SendMessageCommand
			{
				SenderId = senderId,
				ReceiverId = receiverId,
				Message = message
			};

			var result = await _mediator.Send(command);

			if (result.Success)
			{
				// Gửi tin nhắn đến người nhận thông qua SignalR
				await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, message);
			}
		}
	}
}
