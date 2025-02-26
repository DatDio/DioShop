using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Domain.Entities;
using DioShop.Infrastructure.Data;
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

		public ChatMessageRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<ChatMessage>> GetMessagesAsync(string senderId,
			string receiverId)
		{
			return await _dbContext.ChatMessages
				.Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
							(m.SenderId == receiverId && m.ReceiverId == senderId))
				.OrderBy(m => m.Timestamp)
				.ToListAsync();
		}
	}

}
