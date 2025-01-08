using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DioShop.Domain.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public string? AvatarPictureUrl { get; set; }

        // refreshtoken
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }

        public DateTime? LastLogin { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
