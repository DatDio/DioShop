using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.Features.Chats.Requests.Commands;
using DioShop.Application.Ultils;
using DioShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Features.Chats.Handlers.Commands
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ApiResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public SendMessageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ApiResponse<bool>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var messageDto = request.MessageDto;

                // Kiểm tra UserId có hợp lệ không
                var user = await _userManager.FindByIdAsync(messageDto.UserId);
                if (user == null)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Người dùng không tồn tại!",
                        Data = false
                    };
                }

                // Chuyển đổi DTO thành Entity
                var chatMessage = _mapper.Map<ChatMessage>(messageDto);
                chatMessage.IsRead = false; // Tin nhắn mặc định chưa đọc

                await _unitOfWork.ChatMessageRepository.Add(chatMessage);
                await _unitOfWork.Save();

                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "Gửi tin nhắn thành công!",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = $"Lỗi khi gửi tin nhắn: {ex.Message}",
                    Data = false
                };
            }
        }
    }
}
