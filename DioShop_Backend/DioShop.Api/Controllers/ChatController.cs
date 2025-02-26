using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.Brand;
using DioShop.Application.DTOs.ChatMessage;
using DioShop.Application.Features.Brand.Requests.Queries;
using DioShop.Application.Features.Chats.Requests.Commands;
using DioShop.Application.Features.Chats.Requests.Queries;
using DioShop.Application.Features.Coupons.Requests.Commands;
using DioShop.Domain.Entities;
using DioShop.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace DioShop.Api.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("history")]
        [Authorize] // Chỉ user đã đăng nhập mới xem được tin nhắn
        public async Task<ActionResult<List<MessageDto>>> GetChatHistory(string receiverId)
        {
            var senderId = User.Identity.Name; // Lấy ID của người gửi từ token
            //var messages = await _chatMessageRepository.GetMessagesAsync(senderId, receiverId);

            //return Ok(messages);
            var messages = await _mediator.Send(new GetChatHistoryRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId
            });
            return Ok(messages);
        }

        //[HttpPost("send")]
        //[Authorize] // Chỉ user đăng nhập mới gửi được tin nhắn
        //public async Task<IActionResult> SendMessage([FromBody] CreateMessageDto request)
        //{
        //    var command = new SendMessageCommand { MessageDto = request };
        //    var response = await _mediator.Send(command);

        //    return Ok(response);
        //}

    }
}
