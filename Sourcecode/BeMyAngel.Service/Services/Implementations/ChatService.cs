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
    internal class ChatService: IChatService
    {
        private readonly IChatRepository _repository;
        private readonly IChatSessionService _ChatSessionService;
        private readonly IMapper _mapper;

        public ChatService(IChatRepository repository, IChatSessionService ChatSessionService, IMapper mapper)
        {
            _repository = repository;
            _ChatSessionService = ChatSessionService;
            _mapper = mapper;
        }

        private void CheckUserPermission(int ChatId, Session session)
        {
            if (session.UserId == null) //Users without authentication only can request info from the current chat
            {
                var currentChat = GetCurrentBySession(session);
                if (ChatId != currentChat.ChatId)
                    throw new ForbidException($"Session `{session.Token}` can't access chat room `{ChatId}`");
            }
        }

        public Chat GetById(int ChatId, Session session)
        {
            var Chat = _repository.GetById(ChatId);
            CheckUserPermission(Chat.ChatId, session);
            return _mapper.Map<Chat>(Chat);
        }

        public Chat GetByIdentifier(string identifier, Session session)
        {
            var Chat = _repository.GetByIdentifier(identifier);
            CheckUserPermission(Chat.ChatId, session);
            return _mapper.Map<Chat>(Chat);
        }

        public Chat GetCurrentBySession(Session session)
        {
            var ChatSessionsDto = _ChatSessionService.GetBySessionId(session.SessionId);
            if(ChatSessionsDto == null) //If the session doesnt have a chat for it, then create it
            {
                var newChat = new ChatDto
                {
                    CreatedAt = DateTime.Now
                };

                do
                {
                    newChat.Identifier = Guid.NewGuid().ToString().ToUpper();
                }
                while (_repository.GetByIdentifier(newChat.Identifier) != null);

                var newChatDto = _repository.GetById(_repository.Insert(newChat));
                _ChatSessionService.AddSessionToChat(newChatDto.ChatId, session.SessionId);
                return _mapper.Map<Chat>(newChatDto);
            }
            else
                return _mapper.Map<Chat>(_repository.GetById(ChatSessionsDto.ChatId));
            
        }
    }
}
