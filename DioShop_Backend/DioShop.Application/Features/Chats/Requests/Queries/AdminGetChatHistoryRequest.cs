using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.DTOs.ChatMessage;
using DioShop.Application.Ultils;
using MediatR;

namespace DioShop.Application.Features.Chats.Requests.Queries
{
    public class AdminGetChatHistoryRequest : IRequest<ApiResponse<List<ChatHistoryByAdminDto>>>
    {
    }
}
