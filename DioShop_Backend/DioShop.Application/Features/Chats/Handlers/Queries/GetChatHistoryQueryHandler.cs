using AutoMapper;
using DioShop.Application.Contants;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.ChatMessage;
using DioShop.Application.Features.Chats.Requests.Queries;
using DioShop.Application.Ultils;
using DioShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Features.Chats.Handlers.Queries
{
    public class GetChatHistoryQueryHandler : IRequestHandler<GetChatHistoryRequest, ApiResponse<List<MessageDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetChatHistoryQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<List<MessageDto>>> Handle(GetChatHistoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid).Value;
                var messages = await _unitOfWork.ChatMessageRepository.GetMessagesByUserAsync(userId);

                return new ApiResponse<List<MessageDto>>
                {
                    Success = true,
                    Data = _mapper.Map<List<MessageDto>>(messages)
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<MessageDto>>
                {
                    Success = false,
                    Message = $"Lỗi khi lấy lịch sử tin nhắn: {ex.Message}",
                    Data = null
                };
            }
        }
    }

}
