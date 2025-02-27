using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.DTOs.ChatMessage
{
    public class ChatHistoryByAdminDto
    {
        public string UserId { get; set; }  // ID của user
        public string UserName { get; set; } // Tên user (nếu cần)
        public List<MessageDto> Messages { get; set; } = new List<MessageDto>();
    }
}
