using DioShop.Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Domain.Entities
{
	public class ChatMessage : BaseDomainEntity
	{
		public string SenderId { get; set; } // ID người gửi
		public string ReceiverId { get; set; } // ID người nhận
		public string Message { get; set; } // Nội dung tin nhắn

		public bool IsRead { get; set; } = false; // Đã đọc hay chưa
	}
}
