using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.User;

namespace DioShop.Application.Models.Identity
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserDetailDto User { get; set; }
        public DateTime Expiration { get; set; }

    }
}
