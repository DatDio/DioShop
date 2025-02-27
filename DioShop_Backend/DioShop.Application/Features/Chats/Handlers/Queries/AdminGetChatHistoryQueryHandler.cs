using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.Contants;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.ChatMessage;
using DioShop.Application.Features.Chats.Requests.Queries;
using DioShop.Application.Ultils;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DioShop.Application.Features.Chats.Handlers.Queries
{
    public class AdminGetChatHistoryQueryHandler : IRequestHandler<AdminGetChatHistoryRequest, ApiResponse<List<ChatHistoryByAdminDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminGetChatHistoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<List<ChatHistoryByAdminDto>>> Handle(AdminGetChatHistoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Lấy toàn bộ tin nhắn giữa admin và user
                var messages = await _unitOfWork.ChatMessageRepository.GetAllMessagesByAdminAsync();

                // Nhóm tin nhắn theo từng UserId
                var groupedMessages = messages
                    .GroupBy(m => m.UserId)
                    .Select(group => new ChatHistoryByAdminDto
                    {
                        UserId = group.Key,
                        UserName = group.FirstOrDefault()?.User?.UserName, // Lấy tên user từ entity
                        Messages = _mapper.Map<List<MessageDto>>(group.ToList()) // Ánh xạ tin nhắn
                    })
                    .ToList();

                return new ApiResponse<List<ChatHistoryByAdminDto>>
                {
                    Success = true,
                    Data = groupedMessages
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ChatHistoryByAdminDto>>
                {
                    Success = false,
                    Message = $"Lỗi khi lấy lịch sử tin nhắn: {ex.Message}",
                    Data = null
                };
            }
        }
    }


}
