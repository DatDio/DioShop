using DioShop.Application.Contracts.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DioShop.Api.Controllers
{
	[Route("api/chat")]
	[ApiController]
	public class ChatController : ControllerBase
	{
		private readonly IChatMessageRepository _chatMessageRepository;

		public ChatController(IChatMessageRepository chatMessageRepository)
		{
			_chatMessageRepository = chatMessageRepository;
		}

		[HttpGet("history")]
		[Authorize] // Chỉ user đã đăng nhập mới xem được tin nhắn
		public async Task<IActionResult> GetChatHistory(string receiverId)
		{
			var senderId = User.Identity.Name; // Lấy ID của người gửi từ token
			var messages = await _chatMessageRepository.GetMessagesAsync(senderId, receiverId);

			return Ok(messages);
		}
	}
}
