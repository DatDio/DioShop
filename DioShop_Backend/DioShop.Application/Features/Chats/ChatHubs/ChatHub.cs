using DioShop.Application.Contants;
using DioShop.Application.DTOs.ChatMessage;
using DioShop.Application.Features.Chats.Requests.Commands;
using DioShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        public ChatHub(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task SendMessage(CreateMessageDto request)
        {
            var command = new SendMessageCommand { MessageDto = request };
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                // Gửi tin nhắn đến cả admin và user (vì chỉ có 1 user và 1 admin)
                await Clients.User(request.UserId).SendAsync("ReceiveMessage", request.Message, request.IsFromAdmin);
                await Clients.User("Admin").SendAsync("ReceiveMessage", request.Message, request.IsFromAdmin);
            }
        }

    }

}
