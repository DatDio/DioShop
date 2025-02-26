using AutoMapper;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Application.DTOs.ChatMessage;
using DioShop.Application.Features.Chats.Requests.Queries;
using DioShop.Application.Ultils;
using DioShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Features.Chats.Handlers.Queries
{
    public class GetChatHistoryQueryHandler : IRequestHandler<GetChatHistoryRequest,
        ApiResponse<List<MessageDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetChatHistoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<ApiResponse<List<MessageDto>>> Handle(GetChatHistoryRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var messages = await _unitOfWork.ChatMessageRepository.GetMessagesAsync(request.SenderId, request.ReceiverId);
                return new ApiResponse<List<MessageDto>>
                {
                    Success = true,
                    Data = _mapper.Map<List<MessageDto>>(messages)
                };
            }
            catch
            {

            }
            return new ApiResponse<List<MessageDto>>
            {
                Success = true,
                Message = "Something wents wrong,Cannot Get ChatMessage History!",
                Data = null
            };
        }
    }
}
