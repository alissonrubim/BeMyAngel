using AutoMapper;
using BeMyAngel.Service.Models;
using BeMyAngel.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Service.Services.Implementations
{
    internal class ChatRoomService: IChatRoomService
    {
        private readonly IChatRoomRepository _repository;
        private readonly IChatRoomSessionRepository _chatRoomSessionRepository;
        private readonly IMapper _mapper;

        public ChatRoomService(IChatRoomRepository repository, IChatRoomSessionRepository chatRoomSessionRepository, IMapper mapper)
        {
            _repository = repository;
            _chatRoomSessionRepository = chatRoomSessionRepository;
            _mapper = mapper;
        }

        public ChatRoom GetCurrentBySession(Session session)
        {
            var chatRoomSessionsDto = _chatRoomSessionRepository.GetCurrentChatRoomBySessionId(session.SessionId);
            if(chatRoomSessionsDto == null)
            {
                var newChatRoomDto = _repository.GetById(_repository.Insert(new ChatRoomDto
                {
                    CreatedAt = DateTime.Now
                }));
                _chatRoomSessionRepository.AddSessionToChatRoom(newChatRoomDto.ChatRoomId, session.SessionId);
                return _mapper.Map<ChatRoom>(newChatRoomDto);
            }
            else
                return _mapper.Map<ChatRoom>(_repository.GetById(chatRoomSessionsDto.ChatRoomId));
            
        }
    }
}
