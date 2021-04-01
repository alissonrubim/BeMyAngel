using AutoMapper;
using BeMyAngel.Service.Models;
using BeMyAngel.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using BeMyAngel.Persistance.Models;
using BeMyAngel.Service.Exceptions;

namespace BeMyAngel.Service.Services.Implementations
{
    internal class ChatRoomService: IChatRoomService
    {
        private readonly IChatRoomRepository _repository;
        private readonly IChatRoomSessionService _chatRoomSessionService;
        private readonly IMapper _mapper;

        public ChatRoomService(IChatRoomRepository repository, IChatRoomSessionService chatRoomSessionService, IMapper mapper)
        {
            _repository = repository;
            _chatRoomSessionService = chatRoomSessionService;
            _mapper = mapper;
        }

        public ChatRoom GetById(int chatRoomId, Session session)
        {
            if(session.UserId == null) //Users without authentication only can't request info from the current chat
            {
                var currentChatRoom = GetCurrentBySession(session);
                if (chatRoomId != currentChatRoom.ChatRoomId)
                    throw new ForbidException($"Session `{session.Token}` can't access chat room `{chatRoomId}`");
            }
            
            return _mapper.Map<ChatRoom>(_repository.GetById(chatRoomId));
        }

        public ChatRoom GetCurrentBySession(Session session)
        {
            var chatRoomSessionsDto = _chatRoomSessionService.GetBySessionId(session.SessionId);
            if(chatRoomSessionsDto == null) //If the session doesnt have a chat for it, then create it
            {
                var newChatRoomDto = _repository.GetById(_repository.Insert(new ChatRoomDto
                {
                    CreatedAt = DateTime.Now
                }));
                _chatRoomSessionService.AddSessionToChatRoom(newChatRoomDto.ChatRoomId, session.SessionId);
                return _mapper.Map<ChatRoom>(newChatRoomDto);
            }
            else
                return _mapper.Map<ChatRoom>(_repository.GetById(chatRoomSessionsDto.ChatRoomId));
            
        }
    }
}
