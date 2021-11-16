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
        private readonly IChatSessionService _chatSessionService;
        private readonly IMapper _mapper;

        public ChatService(IChatRepository repository, IChatSessionService chatSessionService, IMapper mapper)
        {
            _repository = repository;
            _chatSessionService = chatSessionService;
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
            var chat = _repository.GetById(ChatId);
            CheckUserPermission(chat.ChatId, session);
            return _mapper.Map<Chat>(chat);
        }

        public Chat GetByIdentifier(string identifier, Session session)
        {
            var chat = _repository.GetByIdentifier(identifier);
            CheckUserPermission(chat.ChatId, session);
            return _mapper.Map<Chat>(chat);
        }

        public Chat GetCurrentBySession(Session session)
        {
            var chatSessionsDto = _chatSessionService.GetBySessionId(session.SessionId);
            if(chatSessionsDto != null)
                return _mapper.Map<Chat>(_repository.GetById(chatSessionsDto.ChatId));
            return null;
        }

        public Chat Create(Session session)
        {
            var chat = GetCurrentBySession(session);
            if (chat != null)
                throw new ForbidException($"Session `{session.Token}` can't create a new chat room");

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
            _chatSessionService.AddSessionToChat(newChatDto.ChatId, session.SessionId);
            return _mapper.Map<Chat>(newChatDto);
        }
    }
}
