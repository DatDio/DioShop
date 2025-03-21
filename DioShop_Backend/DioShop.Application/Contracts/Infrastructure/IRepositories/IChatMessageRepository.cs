﻿using DioShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Contracts.Infrastructure.IRepositories
{
	public interface IChatMessageRepository : IGenericRepository<ChatMessage>
	{
		Task<IEnumerable<ChatMessage>> GetMessagesByUserAsync(string userID);

        Task<IEnumerable<ChatMessage>> GetAllMessagesByAdminAsync();
    }
}
