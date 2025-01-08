using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.Models.Identity;
using DioShop.Domain.Entities;

namespace DioShop.Application.Contracts.Infrastructure.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
        Task<IList<ApplicationUser>> ListUser();
        Task<bool> Revoke();
        Task<AuthResponse> Refresh(RefreshRequest model);
        Task<bool> ConfirmEmailAsync(string userId, string code);
    }
}
