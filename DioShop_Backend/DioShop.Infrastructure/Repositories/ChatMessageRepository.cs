using DioShop.Application.Contants;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.ChatMessage;
using DioShop.Domain.Entities;
using DioShop.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Infrastructure.Repositories
{
    public class ChatMessageRepository : GenericRepository<ChatMessage>, IChatMessageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public ChatMessageRepository(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager) : base(dbContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IEnumerable<ChatMessage>> GetAllMessagesByAdminAsync()
        {
            // Lấy admin
            var adminUser = await _userManager.Users
                .Where(u => _userManager.IsInRoleAsync(u, Role.RoleAdmin).Result)
                .FirstOrDefaultAsync();

            if (adminUser == null) return new List<ChatMessage>();

            var adminId = adminUser.Id;

            // Lấy tin nhắn giữa admin và user, kèm thông tin user
            var messages = await _dbContext.ChatMessages
                .Include(m => m.User) // Lấy thông tin người gửi
                .Where(m => m.UserId != null) // Đảm bảo không có UserId null
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();

            return messages;
        }


        public async Task<IEnumerable<ChatMessage>> GetMessagesByUserAsync(string userId)
        {
            var adminUser = await _userManager.Users
                .Where(u => _userManager.IsInRoleAsync(u, Role.RoleAdmin).Result)
                .FirstOrDefaultAsync();

            if (adminUser == null) return new List<ChatMessage>();

            var adminId = adminUser.Id;

            // Lấy tin nhắn giữa admin và user cụ thể
            var messages = await _dbContext.ChatMessages
                .Include(m => m.User) // Lấy thông tin User
                .Where(m => (m.UserId == userId || m.UserId == adminId)) // Lọc theo user hoặc admin
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();

            return messages;
        }

    }

}
