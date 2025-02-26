using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.Commom;

namespace DioShop.Application.DTOs.ChatMessage
{
    public class MessageDto : BaseDto
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Message { get; set; }
    }
}
