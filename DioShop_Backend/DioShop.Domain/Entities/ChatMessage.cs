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
        public string UserId { get; set; }  // Khóa ngoại đến bảng Account/ApplicationUser

        public virtual ApplicationUser User { get; set; } 

        public string Message { get; set; }
        public bool IsFromAdmin { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
