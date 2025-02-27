using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Commom;

namespace DioShop.Application.DTOs.ChatMessage
{
    public class CreateMessageDto : BaseDto
    {
        public string UserId { get; set; } // Ai gửi tin nhắn
        public string Message { get; set; } // Nội dung tin nhắn
        public bool IsFromAdmin { get; set; } // Kiểm tra tin nhắn từ admin
    }
}
